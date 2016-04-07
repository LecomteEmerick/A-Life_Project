using UnityEngine;
using System.Collections.Generic;

public class SoundBankManager : MonoBehaviour {

    private const int SoundBankCapacity = 1;
    private Dictionary<string, HearInfosClass> SoundBank;

	// Use this for initialization
	void Start () {
        this.Initiliaze();
	}

    public void Initiliaze()
    {
        SoundBank = new Dictionary<string, HearInfosClass>(SoundBankCapacity);
        SoundBank.Add("Bounce", new HearInfosClass(15.0f, 0.0f, 0.25f, 0.75f));
    }

    public HearInfosClass GetSound(string soundName)
    {
        if (SoundBank.ContainsKey(soundName))
            return SoundBank[soundName];
        Debug.LogError("Sound " + soundName + " not found on sound bank.");
        return null;
    }
}
