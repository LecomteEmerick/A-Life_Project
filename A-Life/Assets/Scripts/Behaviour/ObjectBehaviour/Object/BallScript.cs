using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {

    public HearInfosClass bounceSound;
    public SoundEmitter soundPlayer;

    // Use this for initialization
    public void Initialize() {
        soundPlayer.Initialize();
        bounceSound = GameData.SoundBankInstance.GetSound(SoundBankManager.SoundName.Bounce);
    }

    void OnCollisionEnter(Collision collision)
    {
        soundPlayer.EmittePonctualSound(bounceSound);
    }
}
