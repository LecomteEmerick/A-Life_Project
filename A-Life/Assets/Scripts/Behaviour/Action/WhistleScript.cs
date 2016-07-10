using UnityEngine;
using System.Collections;
using System;

public class WhistleScript : ActionClass
{

    public AudioSource PlayerAudioSource;

    public AudioClip WistleSound;
    public SoundEmitter Emitter;

    private HearInfosClass WhistleSound;

    public override bool IsFeasible { get { return true; } }

    public override void Initialize()
    {
        this.WhistleSound = new HearInfosClass(50.0f, 3500.0f);
    }

    public override bool CheckCanExecute()
    {
        return true;
    }

    public override void Execute()
    {
        PlayerAudioSource.clip = WistleSound;
        PlayerAudioSource.Play();

        Emitter.EmitteContinueSound(WhistleSound);

        StartCoroutine(StopWhistle());
    }

    IEnumerator StopWhistle()
    {
        yield return new WaitForSeconds(WistleSound.length + 0.5f);
        Emitter.RemoveContinueSound(WhistleSound);
    }
}
