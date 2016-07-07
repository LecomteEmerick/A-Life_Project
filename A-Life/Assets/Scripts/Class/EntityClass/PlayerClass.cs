using UnityEngine;
using System.Collections;

public class PlayerClass : MonoBehaviour {

    public float PlayerSpeed = 40.0f;
    public float RotationSpeed = 37.5f;
    public Transform PlayerTransform;

    public Rigidbody PlayerRigidBody;
    public float JumpForce;
    public Transform Pivot_Transform;
    public Camera PlayerCamera;
}
