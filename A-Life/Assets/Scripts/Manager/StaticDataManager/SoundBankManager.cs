using UnityEngine;
using System.Collections.Generic;

public class SoundBankManager : MonoBehaviour {

    public enum SoundName { Bounce }

    private const int SoundBankCapacity = 1;
    private Dictionary<string, HearInfosClass> SoundBank;

    public void Initiliaze()
    {
        SoundBank = new Dictionary<string, HearInfosClass>(SoundBankCapacity);
        SoundBank.Add(SoundName.Bounce.ToString(), new HearInfosClass(15.0f, 0.0f, 0.25f, 0.75f));
    }

    public HearInfosClass GetSound(SoundName soundName)
    {
        string sound = soundName.ToString();
        if (SoundBank.ContainsKey(sound))
            return SoundBank[sound];
        Debug.LogError("Sound " + soundName + " not found on sound bank.");
        return null;
    }

    //Put here the sound player
    /*
    public void PlaySound(SoundName soundName)
    {
        this.SoundPlayer[soundName.ToString()].play();
    }
    */
}
