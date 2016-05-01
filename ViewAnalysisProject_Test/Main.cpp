#include <iostream>

#include <fstream>

#include "opencv2/opencv.hpp"

#include "OpenCvHandler.h"

int main(int argc, char* argv)
{
	//SetDebugMode();

	std::streampos size;
	std::string line;
	std::ifstream file;

	char * memblock, fileName[200];



	for (int i = 0; i < 42; ++i)
	{
		sprintf_s(fileName, 200, "../data/AssetsDebugImg_%d.png", i % 42);
		file.open(fileName, std::ios::in | std::ios::binary | std::ios::ate);

		if (file.is_open())
		{
			std::cout << "Proceed img Number : " << i << std::endl;

			size = file.tellg();
			memblock = new char[size];
			file.seekg(0, std::ios::beg);
			file.read(memblock, size);
			file.close();

			UnityBinderObject* obj = new UnityBinderObject();

			ProceedTexture(memblock, size, 0, obj);

			DeleteUnityBinder(obj);

			delete[] memblock;

			std::cout << std::endl;
		}
		else {
			std::cout << "error opening file" << std::endl;
		}
	}

	system("pause");
}