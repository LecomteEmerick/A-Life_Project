using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

    public HearInfosClass bounceSound;
    public SoundEmitter soundPlayer;

    // Use this for initialization
    public void Initialize() {
        bounceSound = GameData.SoundBankInstance.GetSound("Bounce");
    }

    void OnCollisionEnter(Collision collision)
    {
        soundPlayer.EmittePonctualSound(bounceSound);
    }
}
