#pragma once

struct UnityBinderObject
{
	int imgSize;
	unsigned char* img;
};

extern "C"
{
	__declspec(dllexport) int ProceedTexture(void* TextureData, int size, int creatureId, UnityBinderObject* outputRes);
	__declspec(dllexport) int GetNextImageInMemory(int creatureId, UnityBinderObject* outputRes);
	__declspec(dllexport) void ClearMemory();
	__declspec(dllexport) void DeleteUnityBinder(UnityBinderObject* binder);
};