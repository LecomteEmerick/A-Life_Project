using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;

public class CreatureClass : EntityClass
{

    public string CreatureName;

    public NavMeshAgent CreatureNavMesh;
    public Animation CreatureAnimator;
    public AnimationBinderClass[] CreatureClipAnimation;

    public PoolledObjectClass CreaturePoolClass;

    public BrainClass CreatureBrain;

    //Receptor
    public SoundReceptor SoundReceptor;
    public SmellReceptor SmellReceptor;
    //public TasteReceptor TasteReceptor;
    //public TouchReceptor TouchReceptor;
    public ViewReceptor ViewReceptor;

    //public List<CreatureScriptBehaviour> CreatureScript;

    public UnityEvent OnHitEvent;

    public AudioSource AudioSource;
    public AudioClip PainClip;
    public AudioClip SighClip;

    //Use this for initialize internal script
    public void Initialize()
    {
        GameData.CreatureManagerInstance.RegisterCreature(this);

        foreach(AnimationBinderClass anim in CreatureClipAnimation)
        {
            CreatureAnimator.AddClip(anim.Animation, anim.AnimationName);
        }
        CreatureAnimator.Play("wait");

        this.SoundReceptor.Initialize();
        //this.TouchReceptor.Initialize();
        this.SmellReceptor.Initialize();
        //this.TasteReceptor.Initialize();
        this.ViewReceptor.Initialize();

        this.CreatureBrain.Initialize();

        //foreach (CreatureScriptBehaviour script in CreatureScript)
        //{
        //    script.Initialize();
        //}
    }

    public string GetCreatureName()
    {
        return this.CreatureName == "" ? this.gameObject.name : this.CreatureName;
    }

    public void OnHitAction()
    {
        CreatureBrain.ChimicalInfos.ChangeValueForChimical(GameData.BrainChimical.Endorphine, 1.5f);
        this.AudioSource.clip = this.PainClip;
        this.AudioSource.Play();
    }

    public void OnComplimentAction()
    {
        CreatureBrain.ChimicalInfos.ChangeValueForChimical(GameData.BrainChimical.Adrenaline, -1.5f);
        this.AudioSource.clip = this.SighClip;
        this.AudioSource.Play();
    }
}
