using UnityEngine;
using System.Collections;

public class CameraCinemaScirpt : MonoBehaviour {

    public GameObject CinemaCamera;
    public float RotationSpeed = 1.0f;
    void Update()
    {
        CinemaCamera.transform.Rotate(new Vector3(0.0f, 0.1f * RotationSpeed, 0.0f));
    }
}
