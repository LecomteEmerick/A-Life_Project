using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SoundEmitter : MonoBehaviour {

    public Transform ObjectTransform;

    private List<HearInfosClass> ContinusSound;

    public void Initialize()
    {
        this.ContinusSound = new List<HearInfosClass>();
    }

    public void EmittePonctualSound(HearInfosClass sound)
    {
        //CARE MEMORY USAGE (THREAD?)
        List<CreatureClass> CreatureData = GameData.CreatureManagerInstance.SphereCastCreature(ObjectTransform.position, sound.Volume);
        foreach (CreatureClass creature in CreatureData)
            creature.SoundReceptor.Receptor.Reception(sound, ObjectTransform.position);
    }

    public void EmitteContinueSound(HearInfosClass sound)
    {
        ContinusSound.Add(sound);
    }

    public void RemoveContinueSound(HearInfosClass sound)
    {
        ContinusSound.Remove(sound);
    }

    void FixedUpdate()
    {
        foreach(HearInfosClass sound in this.ContinusSound)
        {
            EmittePonctualSound(sound);
        }
    }
}
