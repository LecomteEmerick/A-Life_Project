using UnityEngine;
using System.Collections;

public class PlayerClass : EntityClass {

    public float PlayerSpeed = 40.0f;
    public float RotationSpeed = 37.5f;
    public float JumpForce;

    public Transform Pivot_Transform;

    public PlayerMoveScript PlayerMovementScript;
    public PlayerActionManagerScript PlayerActionManager;

    public AudioSource PlayerAudioSource;

    public bool HasControl = true;

    public void Initialize()
    {
        Debug.Log("Reactive Initialize Player Movement script pour la realese.");
        //this.PlayerMovementScript.Initialize();
        this.PlayerActionManager.Initialize();
    }
}
