using UnityEngine;
using System.Collections;

public class PlayerMoveScript : MonoBehaviour {

    public bool HasControl = true;

    public PlayerClass playerInfos;
    public int TerrainLayerNumber = 9;

    private bool isJumping = false;

	// Use this for initialization
	public void Initialize () {
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(HasControl)
            InputHandler();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isJumping && collision.gameObject.layer == TerrainLayerNumber)
            isJumping = false;
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

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            playerInfos.PlayerRigidBody.AddForce( new Vector3(0.0f,10.0f,0.0f) * playerInfos.JumpForce);
        }

        if (Input.GetAxis("Camera X") != 0.0f)
        {
            playerInfos.Pivot_Transform.eulerAngles += Vector3.up * Input.GetAxis("Camera X") * Time.deltaTime * playerInfos.RotationSpeed;
        }

        if (Input.GetAxis("Camera Y") != 0.0f)
        {

            float mouseDirection = Input.GetAxis("Camera Y");

            playerInfos.Pivot_Transform.eulerAngles -= Vector3.right * mouseDirection * Time.deltaTime * playerInfos.RotationSpeed;

            if (playerInfos.Pivot_Transform.eulerAngles.x > 55.0f && mouseDirection < 0.0f)
                playerInfos.Pivot_Transform.eulerAngles = new Vector3(55.0f, playerInfos.Pivot_Transform.eulerAngles.y, playerInfos.Pivot_Transform.eulerAngles.z);

            else if (playerInfos.Pivot_Transform.eulerAngles.x > 55.0f && mouseDirection > 0.0f)
                playerInfos.Pivot_Transform.eulerAngles = new Vector3(0.0f, playerInfos.Pivot_Transform.eulerAngles.y, playerInfos.Pivot_Transform.eulerAngles.z);
        }


    }
}
