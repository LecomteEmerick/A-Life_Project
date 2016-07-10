using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

    public Rigidbody BallRigidbody;
    public HearInfosClass bounceSound;
    public SoundEmitter soundPlayer;

    public AudioSource ballAudioSource;
    public AudioClip BallClip;
    public float BallPushForce = 500.0f;
    public float BallUpMultiplier = 2.0f;

    // Use this for initialization
    public void Initialize() {
        soundPlayer.Initialize();
        bounceSound = GameData.SoundBankInstance.GetSound(SoundBankManager.SoundName.Bounce);
        ballAudioSource.clip = BallClip;
    }

    void OnCollisionEnter(Collision collision)
    {
        ballAudioSource.Play();
        soundPlayer.EmittePonctualSound(bounceSound);
    }

    private void ShootBall(Vector3 direction)
    {
        BallRigidbody.AddForce((direction + Vector3.up * BallUpMultiplier) * BallPushForce);
    }

    public void PlayBall()
    {
        ShootBall(GameData.ActiveCamera.transform.forward);
    }

    public void PlayBall(CreatureClass entity)
    {
        ShootBall(entity.transform.forward);
    }
}
