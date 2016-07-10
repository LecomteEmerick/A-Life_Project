using UnityEngine;
using System.Collections;
using System;

public class ComplimentScript : ActionClass {

    public PlayerClass PlayerInfos;
    public AudioClip ComplimentClip;

    public override void Initialize()
    {
        
    }

    public override bool CheckCanExecute()
    {
        return true;
    }

    public override void Execute()
    {
        PlayerInfos.PlayerAudioSource.clip = ComplimentClip;
        PlayerInfos.PlayerAudioSource.Play();
    }


}
