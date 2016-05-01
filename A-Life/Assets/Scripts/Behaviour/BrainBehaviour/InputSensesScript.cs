using UnityEngine;
using System.Collections;

public class InputSensesScript : CreatureScriptBehaviour
{

    private BrainClass Brain;

    public override void Initialize()
    {
        this.Brain = new BrainClass();
        this.Brain.Construct();
    }

    /// <summary>
    /// Use this function for the Taste and Touch
    /// </summary>
    /// <param name="infos"></param>
    public void SensorialInformationReceiver(SensorialClass infos)
    {
        switch (infos.Type)
        {
            case SensesType.Taste:
                TreatTastingInfos((TasteInfosClass)infos);
                break;
            case SensesType.Touch:
                TreatTouchingInfos((TouchInfosClass)infos);
                break;
            default:
                Debug.LogError("BAD SENSORIAL INFOS RECEIVE ON BRAIN FUNCTION (Taste and touch).");
                break;
        }
    }

    /// <summary>
    /// Use this function for the smell/hear and view the position is needed
    /// </summary>
    /// <param name="infos"></param>
    /// <param name="Position"></param>
    public void SensorialInformationReceiver(SensorialClass infos, Vector3 Position)
    {
        switch (infos.Type)
        {
            case SensesType.Hearing:
                TreatHearingInfos((HearInfosClass)infos, Position);
                break;
            case SensesType.Smell:
                TreatSmellingInfos((SmellInfosClass)infos, Position);
                break;
            case SensesType.View:
                TreatViewingInfos((ViewInfosClass)infos, Position);
                break;
            default:
                Debug.LogError("BAD SENSORIAL INFOS RECEIVE ON BRAINFUNCTION (Hear / smell / View).");
                break;
        }
    }

    private void TreatHearingInfos(HearInfosClass hear,Vector3 Position)
    {
        Debug.Log(hear.printInfos());
    }

    private void TreatSmellingInfos(SmellInfosClass smell, Vector3 Position)
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

    private void TreatViewingInfos(ViewInfosClass view, Vector3 Position)
    {
        Debug.Log("View infos receive.");
    }
}
