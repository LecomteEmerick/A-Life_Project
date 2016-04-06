using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {
    public Rigidbody myRigidBody;

    public HearInfosClass bounceSound;
    public HearInfosClass musicSound;

    public SoundEmitter soundPlayer;

    // Use this for initialization
    void Start () {
        bounceSound = new HearInfosClass(15.0f, 0.0f, 0.25f, 0.75f);
        musicSound = new HearInfosClass(75.0f, 25.0f, 25.0f, 25.0f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        soundPlayer.EmittePonctualSound(bounceSound);
        //soundPlayer.EmitteContinueSound(musicSound);
        myRigidBody.AddForce(0.0f, 500.0f, 0.0f);
    }
}
