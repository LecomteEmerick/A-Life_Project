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
        this.receptor.UpdateFunction = this.SmellReceptorUpdateFunction;
    }



    void SmellReceptorUpdateFunction(SmellInfosClass infos)
    {
        BrainHandler.SensorialInformationReceiver(infos);
    }

}
