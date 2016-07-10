using UnityEngine;
using System.Collections;

public class PlayerMoveScript : MonoBehaviour {

    public PlayerClass playerInfos;
    public int TerrainLayerNumber = 9;

    public GameObject GroundChecker;

    public Animator PlayerAnimator;

    private bool isJumping = false;

	// Use this for initialization
	public void Initialize () {
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (playerInfos.HasControl)
        {
            checkGround();
            MoveInputHandler();
        }
        else
        {
            PlayerAnimator.SetFloat("Turn", 0.0f);
            PlayerAnimator.SetFloat("Forward", 0.0f);
            if (isJumping)
            {
                playerInfos.EntityRigidBody.AddForce((Physics.gravity * 2.0f) - Physics.gravity);
                PlayerAnimator.SetFloat("Jump", playerInfos.EntityRigidBody.velocity.y);
            }
        }
    }

    void checkGround()
    {
        RaycastHit hitInfo;
        
        if (Physics.Raycast(GroundChecker.transform.position, Vector3.down, out hitInfo, 0.5f))
        {
            isJumping = false;
            PlayerAnimator.SetBool("OnGround", true);
            PlayerAnimator.applyRootMotion = true;
        }
        else
        {
            //Debug.DrawRay(GroundChecker.transform.position, Vector3.down * 0.1f, Color.blue, 9999999);
            isJumping = true;
            PlayerAnimator.SetBool("OnGround", false);
            PlayerAnimator.applyRootMotion = false;
        }
    }

    void MoveInputHandler()
    {

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            PlayerAnimator.SetFloat("Forward", 0.0f);
            PlayerAnimator.SetBool("OnGround", false);
            playerInfos.EntityRigidBody.velocity = new Vector3(0.0f, playerInfos.JumpForce, 0.0f);
            PlayerAnimator.applyRootMotion = false;
            PlayerAnimator.SetFloat("Jump", playerInfos.EntityRigidBody.velocity.y);
        }
        else if (isJumping)
        {
            PlayerAnimator.SetFloat("Forward", 0.0f);
            playerInfos.EntityTransform.position += playerInfos.EntityTransform.forward * Input.GetAxis("Vertical") * Time.deltaTime * (playerInfos.PlayerSpeed);
            playerInfos.EntityRigidBody.AddForce((Physics.gravity * 2.0f) - Physics.gravity);
            PlayerAnimator.SetFloat("Jump", playerInfos.EntityRigidBody.velocity.y);
        }

        if(!isJumping)
        {
            PlayerAnimator.SetFloat("Turn", Input.GetAxis("Horizontal"));
            PlayerAnimator.SetFloat("Forward", Input.GetAxis("Vertical") * playerInfos.PlayerSpeed);
        }

        if(Input.GetAxis("Vertical") < 0.0f)
        {
            playerInfos.EntityTransform.position += playerInfos.EntityTransform.forward * Input.GetAxis("Vertical") * Time.deltaTime * playerInfos.PlayerSpeed;
        }

        if (Input.GetAxis("Camera X") != 0.0f)
        {
            playerInfos.EntityTransform.eulerAngles += Vector3.up * Input.GetAxis("Camera X") * Time.deltaTime * playerInfos.RotationSpeed;
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
