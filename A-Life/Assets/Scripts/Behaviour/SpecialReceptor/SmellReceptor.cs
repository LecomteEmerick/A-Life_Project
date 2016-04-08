using UnityEngine;
using System.Collections;

public class SmellReceptor : MonoBehaviour {

    [SerializeField]
    private InputSensesScript BrainHandler;

    private ReceptorClass<SmellInfosClass> receptor;

    public ReceptorClass<SmellInfosClass> Receptor
    {
        get
        {
            return receptor;
        }
    }

    public void Initialize()
    {
        this.receptor = new ReceptorClass<SmellInfosClass>();
        this.receptor.UpdateEmittorFunction = this.SmellReceptorUpdateFunction;
    }



    void SmellReceptorUpdateFunction(SmellInfosClass infos, Vector3 EmittorPosition)
    {
        BrainHandler.SensorialInformationReceiver(infos, EmittorPosition);
    }

}
