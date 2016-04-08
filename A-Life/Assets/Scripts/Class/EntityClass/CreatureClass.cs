using UnityEngine;
using System.Collections.Generic;

public class CreatureClass : MonoBehaviour {

    public string CreatureName;

    public Transform CreatureTransform;
    public Collider Collider;

    //Receptor
    public SoundReceptor SoundReceptor;
    public SmellReceptor SmellReceptor;
    public TasteReceptor TasteReceptor;
    public TouchReceptor TouchReceptor;
    public ViewReceptor ViewReceptor;

    public List<CreatureScriptBehaviour> CreatureScript;

    //Use this for initialize internal script
    public void Initialize()
    {
        this.SoundReceptor.Initialize();
        this.ViewReceptor.Initialize();
        this.TouchReceptor.Initialize();
        this.SmellReceptor.Initialize();
        this.TasteReceptor.Initialize();

        foreach (CreatureScriptBehaviour script in CreatureScript)
        {
            script.Initialize();
        }
    }

}
