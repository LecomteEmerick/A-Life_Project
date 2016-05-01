#include "OpenCvHandler.h"

#include <fstream>
#include <vector>

#include "opencv2/opencv.hpp"
#include "opencv2/features2d.hpp"

bool isInit = false;
std::ofstream debugFile;
cv::Ptr<cv::ORB> orbRef = cv::ORB::create(500, 1.200000048F, 8, 0);
cv::Ptr<cv::DescriptorMatcher> matcher = cv::DescriptorMatcher::create("BruteForce-Hamming"); //BruteForce-Hamming

cv::Size trainingSize(512, 512);

struct DistanceByImg
{
	int ImgIndex;
	float averageDistance;
};

struct MatchedObject
{
	double minDist;
	double GoodPercentage;
};

struct ViewedObject
{
	cv::Mat refImg;
	//std::vector<cv::Mat> img;
	std::vector<int> DescriptorImgIndex;

	ViewedObject(){}
	ViewedObject(cv::Mat ref_Img, int descriptorIndex=-1) : refImg(ref_Img)
	{
		DescriptorImgIndex.push_back(descriptorIndex);
		//img.push_back(ref_Img);
	}
};

struct CreatureVisualMemory
{
	int Id;
	std::vector<ViewedObject> ExtractedObject;
};

std::vector<CreatureVisualMemory> CreatureMemory;

cv::Mat ClassifyObject(int id, const cv::Mat& baseImg, bool isComplete)
{

	if (id+1 > CreatureMemory.size())
	{
		CreatureMemory.resize(id+1);
	}

	if (CreatureMemory[id].ExtractedObject.size() < 1)
	{
		if(isComplete)
			CreatureMemory[id].ExtractedObject.push_back(ViewedObject(baseImg));
		return baseImg;
	}

	cv::Mat refImgGray, baseImgGray, basedImgResized, refImg, img_matches, descriptorBaseImg, descriptorRefImg, outputImg;
	std::vector<cv::DMatch> matches, goodMatches;
	std::vector<cv::KeyPoint> keyPoints, keyPoints2;

	//cv::resize(baseImg, basedImgResized, refImg.size());
	cv::cvtColor(baseImg, baseImgGray, CV_RGB2GRAY);

	orbRef->detect(baseImgGray, keyPoints2);
	orbRef->compute(baseImgGray, keyPoints2, descriptorBaseImg);

	if (keyPoints2.size() < 1)
	{
		debugFile << "Error on keypoint for base img" << std::endl;
		return baseImg;
	}

	std::vector<MatchedObject> matchedObjectList(CreatureMemory[id].ExtractedObject.size());

	for (int i = 0; i < CreatureMemory[id].ExtractedObject.size(); ++i)
	{

		goodMatches.clear();
		matches.clear();
		keyPoints.clear();

		refImg = CreatureMemory[id].ExtractedObject[i].refImg;		

		cv::cvtColor(refImg, refImgGray, CV_RGB2GRAY);

		orbRef->detect(refImgGray, keyPoints);
		orbRef->compute(refImgGray, keyPoints, descriptorRefImg);

		matcher->match(descriptorRefImg, descriptorBaseImg, matches);

		double max_dist = 0; double min_dist = 100;

		for (int i = 0; i < matches.size(); ++i)
		{
			double dist = matches[i].distance;
			if (dist < min_dist) min_dist = dist;
			if (dist > max_dist) max_dist = dist;
		}

		for (int i = 0; i < matches.size(); ++i)
		{
			if (matches[i].distance <= 4 * min_dist)
			{
				goodMatches.push_back(matches[i]);
			}
		}

		matchedObjectList[i].GoodPercentage = ((double)goodMatches.size() / (double)matches.size()) * 100.0;
		matchedObjectList[i].minDist = min_dist;

	}

	int index = -1;
	MatchedObject obj;
	obj.GoodPercentage = 10.0;
	obj.minDist = 10.0;
	for (int i = 0; i < matchedObjectList.size(); ++i)
	{
		if (matchedObjectList[i].minDist < obj.minDist)
		{
			index = i;
			obj = matchedObjectList[i];
		}
		else if (matchedObjectList[i].minDist < obj.minDist + 5)
		{
			if (matchedObjectList[i].GoodPercentage > obj.GoodPercentage)
			{
				index = i;
				obj = matchedObjectList[i];
			}
		}
	}

	if (index > -1)
	{
		//CreatureMemory[id].ExtractedObject[index].img.push_back(baseImg);
		return CreatureMemory[id].ExtractedObject[index].refImg;
	}
	else
	{
		if(isComplete)
			CreatureMemory[id].ExtractedObject.push_back(ViewedObject(baseImg));
		return baseImg;
	}
}

void CreateUnityObject(ViewedObject* obj, UnityBinderObject* output)
{
	std::vector<unsigned char> data;
	if (!cv::imencode(".PNG", obj->refImg, data))
	{
		debugFile << "Error on imencode" << std::endl;
		return;
	}

	output->imgSize = data.size();
	output->img = (unsigned char*)malloc(sizeof(unsigned char) * output->imgSize);
	std::memcpy(output->img, data.data(), sizeof(unsigned char) * output->imgSize);
}

cv::Mat ClassifyObject_V2(int id, const cv::Mat& baseImg, bool isComplete);

ViewedObject* FloodFillMethod(cv::Mat& imgColored, int creatureId)
{
	uchar Lbl = 1;
	ViewedObject* obj = nullptr;
	bool isComplete = true;

	cv::Mat thresholdedImg;
	cv::cvtColor(imgColored, thresholdedImg, CV_RGB2GRAY);
	cv::threshold(thresholdedImg, thresholdedImg, 0, 255, CV_THRESH_BINARY);

	cv::Size imgSize = thresholdedImg.size();

	for (int j = 0; j < imgSize.height; ++j)
	{
		for (int i = 0; i < imgSize.width; ++i)
		{
			if (thresholdedImg.at<uchar>(j, i) >= Lbl)
			{
				cv::Rect r;
				cv::floodFill(thresholdedImg, cv::Point(i, j), Lbl, &r);
				++Lbl;

				if (r.area() > 100)
				{
					if(obj == nullptr)
						obj = new ViewedObject();

					isComplete = !(r.x == 0 || r.width + r.x == thresholdedImg.cols || r.y == 0 || r.y + r.height == thresholdedImg.rows);

					cv::Mat extractedImg = imgColored(r);


					cv::Mat m = ClassifyObject(creatureId, extractedImg, isComplete);
					m.copyTo(obj->refImg);
				}
			}
		}
	}

	return obj;
}



int ProceedTexture(void* TextureData, int size, int creatureId, UnityBinderObject* outputRes )
{
	if (!isInit)
	{
		isInit = true;
		debugFile.open("RenderToTextureLog.txt", std::ios::out | std::ios::trunc);
	}

	cv::Mat rawData = cv::Mat(1, size, CV_8UC1, TextureData);
	cv::Mat img = cv::imdecode(rawData, CV_LOAD_IMAGE_COLOR);

	ViewedObject* obj = FloodFillMethod(img, creatureId);

	if (obj == nullptr)
		return 0;

	CreateUnityObject(obj, outputRes);

	delete obj;

	return 1;
}

int currentMemoryIndex = -1;
int GetNextImageInMemory(int creatureId, UnityBinderObject* outputRes)
{
	if (CreatureMemory.size() < 1 || CreatureMemory[creatureId].ExtractedObject.size() < 1)
		return -1;

	++currentMemoryIndex;
	currentMemoryIndex = currentMemoryIndex % CreatureMemory[creatureId].ExtractedObject.size();

	CreateUnityObject(&CreatureMemory[creatureId].ExtractedObject[currentMemoryIndex], outputRes);
	return currentMemoryIndex;
}

void ClearMemory()
{
	currentMemoryIndex = -1;
	CreatureMemory.clear();
}

void DeleteUnityBinder(UnityBinderObject* binder)
{
	free(binder->img);
	delete binder;
}

int currentDescriptorIndex = 0;
cv::Mat ClassifyObject_V2(int id, const cv::Mat& baseImg, bool isComplete)
{

	if (id + 1 > CreatureMemory.size())
	{
		CreatureMemory.resize(id + 1);
	}

	cv::Mat baseImgGray, descriptorBaseImg;
	std::vector<cv::KeyPoint> keyPoints;

	cv::resize(baseImg, baseImgGray, trainingSize);
	cv::cvtColor(baseImgGray, baseImgGray, CV_RGB2GRAY);

	orbRef->detect(baseImgGray, keyPoints);
	orbRef->compute(baseImgGray, keyPoints, descriptorBaseImg);

	if (keyPoints.size() < 1)
	{
		debugFile << "Error on keypoint for base img" << std::endl;
		return baseImg;
	}

	if (CreatureMemory[id].ExtractedObject.size() < 1)
	{
		if (isComplete)
			CreatureMemory[id].ExtractedObject.push_back(ViewedObject(baseImg, currentDescriptorIndex));
		++currentDescriptorIndex;
		matcher->add(descriptorBaseImg);
		matcher->train();
		return baseImg;
	}

	std::vector<cv::DMatch> matches;
	
	matcher->match(descriptorBaseImg, matches);

	std::map<int, float> DistancebyImg;
	std::map<int, int> NumberMatchesByImg;
	for (int i = 0; i < matches.size(); ++i)
	{
		DistancebyImg[matches[i].imgIdx] += matches[i].distance;
		NumberMatchesByImg[matches[i].imgIdx] += 1;
	}

	for (auto index : DistancebyImg)
	{
		//std::cout << "distance total : " << index.second << " number element : " << NumberMatchesByImg[index.first] << std::endl;
		DistancebyImg[index.first] /= NumberMatchesByImg[index.first];
	}

	int minAverageDist = 100;
	int ImgIndex = -1;
	for (auto index : DistancebyImg)
	{
		//std::cout << "Image Number : " << index.first << " distance : " << index.second << std::endl;
		if (minAverageDist > index.second)
		{
			ImgIndex = index.first;
			minAverageDist = index.second;
		}
	}

	//std::cout << "average distance : " << minAverageDist << std::endl;

	int index = 0;
	if (minAverageDist < 35.0f)
	{
		bool found = false;
		for (int index = 0; index < CreatureMemory[id].ExtractedObject.size(); ++index)
		{
			for (int j = 0; j < CreatureMemory[id].ExtractedObject[index].DescriptorImgIndex.size(); ++j)
			{
				if (CreatureMemory[id].ExtractedObject[index].DescriptorImgIndex[j] == ImgIndex)
				{
					//std::cout << "Found for img index : " << index << std::endl;
					//std::cout << "Found for descriptor : " << j << std::endl;
					CreatureMemory[id].ExtractedObject[index].DescriptorImgIndex.push_back(currentDescriptorIndex);
					++currentDescriptorIndex;
					found = true;
					break;
				}
			}
			if (found)
				break;
		}

	}
	else {
		//std::cout << "Not found img !!!!!!!!!!!!!!!" << std::endl;
		if (isComplete)
		{
			CreatureMemory[id].ExtractedObject.push_back(ViewedObject(baseImg, currentDescriptorIndex));
			index = CreatureMemory[id].ExtractedObject.size() - 1;
			++currentDescriptorIndex;
		}
		else {
			return baseImg;
		}
	}

	matcher->add(descriptorBaseImg);
	matcher->train();

	return CreatureMemory[id].ExtractedObject[index].refImg;
}