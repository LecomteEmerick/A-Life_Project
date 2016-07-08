using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.IO;

public class CameraRenderToTextureTest : MonoBehaviour {

    [StructLayout(LayoutKind.Sequential)]
    public struct UnityOpenCVBinder
    {
        public int imgSize;
        public System.IntPtr img;
    }

    //[DllImport("ViewAnalysisProject")]
    //public extern static System.IntPtr ProceedTexture(System.IntPtr texturePtr, int size);

    [DllImport("ViewAnalysisProject")]
    public extern static int ProceedTexture(System.IntPtr TextureData, int size, int creatureId, ref UnityOpenCVBinder outputRes);

    [DllImport("ViewAnalysisProject")]
    public extern static int GetNextImageInMemory(int creatureId, ref UnityOpenCVBinder outputRes);

    [DllImport("ViewAnalysisProject")]
    public extern static void ClearMemory();

    [DllImport("ViewAnalysisProject")]
    public extern static void DeleteUnityBinder(ref UnityOpenCVBinder binder);

    public RenderTexture text;
    public Renderer OutPutRenderer;
    public Renderer MemoryRenderer;
    //public TextMesh RealTimeDebug;

    public Texture2D NoImage;

    int NumberPoceed = 0;
    bool isTextureAssigned = false;

    private bool ProceedFrame = false;

    void Start()
    {
        ClearMemory();
        InvokeRepeating("ProceedFrameChangeStatus", 0.0f, 1.0f);
    }

    void ProceedFrameChangeStatus()
    {
        ProceedFrame = true;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
            GetNextMemoryInput();

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    ClearMemory();
        //    Debug.Log("Memory cleared.");
        //}

        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    OutPutRenderer.material.mainTexture = null;
        //}
    }

    void OnPostRender() 
    {
        if (ProceedFrame)
        {
            ProceedFrame = false;
            SendToDll();
        }
    }

    void GetNextMemoryInput()
    {
        UnityOpenCVBinder bind = new UnityOpenCVBinder();

        int isGood = GetNextImageInMemory(0, ref bind);
        if (isGood == -1)
        {
            Debug.Log("No image in memory.");
            return;
        }

        //Convertion de la valeur de retour de la fonction en Byte array
        byte[] managedArray = new byte[bind.imgSize];
        Marshal.Copy(bind.img, managedArray, 0, bind.imgSize);

        //Affichage de la texture
        Destroy(MemoryRenderer.material.mainTexture);

        Texture2D tex = new Texture2D(text.width, text.height);
        tex.LoadImage(managedArray);
        MemoryRenderer.material.mainTexture = tex;

    }

    //int fileIndex = 0;
    void SendToDll()
    {

        //Get du render To Texture
        RenderTexture.active = text;
        Texture2D text2 = new Texture2D(text.width, text.height, TextureFormat.ARGB32, false);
        text2.ReadPixels(new Rect(0, 0, text.width, text.height), 0, 0);
        text2.Apply();

        //Convertion de la valeur d'entrée en byte array puis en bytePtr
        byte[] data = text2.EncodeToPNG();
        System.IntPtr dataPtr = Marshal.UnsafeAddrOfPinnedArrayElement(data, 0);

        Destroy(text2);

        UnityOpenCVBinder bind = new UnityOpenCVBinder();

        //Appel de la fonction DLL
        int isGood = ProceedTexture(dataPtr, data.Length, 0, ref bind);
        if(isGood == 0)
        {
            if (isTextureAssigned)
            {
                Destroy(OutPutRenderer.material.mainTexture);
                isTextureAssigned = false;
            }
            OutPutRenderer.material.mainTexture = NoImage;
            //Debug.Log("Proceed Texture return nullptr.");
            return;
        }

        //File.WriteAllBytes("../data/AssetsDebugImg_" + fileIndex + ".png", data);
        //++fileIndex;

        //Convertion de la valeur de retour de la fonction en Byte array
        byte[] managedArray = new byte[bind.imgSize];
        Marshal.Copy(bind.img, managedArray, 0, bind.imgSize);

        DeleteUnityBinder(ref bind);

        //Affichage de la texture
        if(isTextureAssigned)
            Destroy(OutPutRenderer.material.mainTexture);

        Texture2D tex = new Texture2D(text.width, text.height);
        tex.LoadImage(managedArray);
        OutPutRenderer.material.mainTexture = tex;

        isTextureAssigned = true;

        //RealTimeDebug.text = "Display frame : " + NumberPoceed;
        ++NumberPoceed;
    }

}
