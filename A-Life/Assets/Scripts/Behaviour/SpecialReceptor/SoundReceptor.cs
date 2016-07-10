using UnityEngine;
using System.Collections;

public class SoundReceptor : MonoBehaviour {

    [SerializeField]
    private InputSensesScript BrainHandler;

    private ReceptorClass<HearInfosClass> receptor;

    public ReceptorClass<HearInfosClass> Receptor
    {
        get
        {
            return receptor;
        }
    }

    public void Initialize()
    {
        this.receptor = new ReceptorClass<HearInfosClass>();
        this.receptor.UpdateFunction = this.SoundReceptorUpdateFunction;
    }



    void SoundReceptorUpdateFunction(HearInfosClass infos)
    {
        BrainHandler.SensorialInformationReceiver(infos);
    }

}
