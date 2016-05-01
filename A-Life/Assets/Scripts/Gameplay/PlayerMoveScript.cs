using UnityEngine;
using System.Collections;

public class PlayerMoveScript : MonoBehaviour {

    public PlayerClass playerInfos;

	// Use this for initialization
	void Start () {
        //Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        InputHandler();
    }

    void InputHandler()
    {
        Vector3 cameraVectorRight = playerInfos.PlayerCamera.transform.right;
        cameraVectorRight.y = 0.0f;
        Vector3 cameraVectorForward = playerInfos.PlayerCamera.transform.forward;
        cameraVectorForward.y = 0.0f;
        if (Input.GetAxis("Horizontal") != 0.0f)
        {
            playerInfos.PlayerTransform.position += cameraVectorRight.normalized * Input.GetAxis("Horizontal") * Time.deltaTime * playerInfos.PlayerSpeed;
        }

        if (Input.GetAxis("Vertical") != 0.0f)
        {
            playerInfos.PlayerTransform.position += cameraVectorForward.normalized * Input.GetAxis("Vertical") * Time.deltaTime * playerInfos.PlayerSpeed;
        }

        if(Input.GetAxis("Camera X") != 0.0f)
        {
            playerInfos.Pivot_Transform.eulerAngles += Vector3.up * Mathf.Clamp(Input.GetAxis("Camera X"), -1.0f, 1.0f) * Time.deltaTime * playerInfos.RotationSpeed;
        }

        //if (Input.GetAxis("Camera Y") != 0.0f)
        //{
        //    if(!(playerInfos.Pivot_Transform.eulerAngles.x >= 50.0f) || !(Input.GetAxis("Camera Y")>0))
        //        playerInfos.Pivot_Transform.eulerAngles += Vector3.right * Mathf.Clamp(Input.GetAxis("Camera Y"), -1.0f, 1.0f) * Time.deltaTime * playerInfos.RotationSpeed;

        //    if (playerInfos.Pivot_Transform.eulerAngles.x > 55.0f)
        //        playerInfos.Pivot_Transform.eulerAngles = new Vector3(0.0f, playerInfos.Pivot_Transform.eulerAngles.y, playerInfos.Pivot_Transform.eulerAngles.z);
        //}


    }
}
