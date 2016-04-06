using UnityEngine;
using System.Collections;

public class BrainScript : MonoBehaviour {

    private BrainClass Brain;

    void Start()
    {
        this.Brain = new BrainClass();
        this.Brain.Construct();
    }

    public void SensorialInformationReceiver(SensorialClass infos)
    {
        switch(infos.Type)
        {
            case SensesType.Hearing:
                TreatHearingInfos((HearInfosClass)infos);
                break;
            case SensesType.Smell:
                TreatSmellingInfos((SmellInfosClass)infos);
                break;
            case SensesType.Taste:
                TreatTastingInfos((TasteInfosClass)infos);
                break;
            case SensesType.Touch:
                TreatTouchingInfos((TouchInfosClass)infos);
                break;
            case SensesType.View:
                TreatViewingInfos((ViewInfosClass)infos);
                break;
            default:
                Debug.LogError("BAD SENSORIAL INFOS RECEIVE ON BRAIN.");
                break;
        }
    }

    private void TreatHearingInfos(HearInfosClass hear)
    {
        Debug.Log(hear.printInfos());
    }

    private void TreatSmellingInfos(SmellInfosClass smell)
    {
        Debug.Log("Smell infos receive on brain.");
    }

    private void TreatTastingInfos(TasteInfosClass taste)
    {
        Debug.Log("Taste infos receive on brain.");
    }

    private void TreatTouchingInfos(TouchInfosClass touch)
    {
        Debug.Log("Touch infos receive.");
    }

    private void TreatViewingInfos(ViewInfosClass view)
    {
        Debug.Log("View infos receive.");
    }
}
