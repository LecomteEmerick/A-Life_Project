using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IdentifiateObject
{
    public Vector3 PossibleArea;
    public List<HearInfosClass> HearedInfos;
    public List<SmellInfosClass> SmelledInfos;
    public List<ViewInfosClass> ViewedInfos;

    public IdentifiateObject()
    {
        this.HearedInfos = new List<HearInfosClass>();
        this.SmelledInfos = new List<SmellInfosClass>();
        this.ViewedInfos = new List<ViewInfosClass>();
    }

    public int GetInterestRate()
    {
        return this.HearedInfos.Count + this.SmelledInfos.Count + this.ViewedInfos.Count;
    }
}

public class InputSensesScript : CreatureScriptBehaviour
{
    public CreatureClass CreatureInstance;

    public List<HearInfosClass> CurrentHearInfos;
    public List<SmellInfosClass> CurrentSmellInfos;
    public List<ViewInfosClass> CurrentViewInfos;

    public float VectorRadiusAcceptable = 0.5f;

    public override void Initialize()
    {
        this.CurrentHearInfos = new List<HearInfosClass>();
        this.CurrentSmellInfos = new List<SmellInfosClass>();
        this.CurrentViewInfos = new List<ViewInfosClass>();

        InvokeRepeating("UpdateSenses", GameData.SensesStartDelay, GameData.SensesUpdateDelay);
    }

    /// <summary>
    /// Use this function for the smell/hear and view the position is needed
    /// </summary>
    /// <param name="infos"></param>
    /// <param name="Position"></param>
    public void SensorialInformationReceiver(SensorialClass infos)
    {
        switch (infos.Type)
        {
            case SensesType.Hearing:
                TreatHearingInfos((HearInfosClass)infos);
                break;
            case SensesType.Smell:
                TreatSmellingInfos((SmellInfosClass)infos);
                break;
            case SensesType.View:
                TreatViewingInfos((ViewInfosClass)infos);
                break;
            case SensesType.Taste:
                TreatTastingInfos((TasteInfosClass)infos);
                break;
            case SensesType.Touch:
                TreatTouchingInfos((TouchInfosClass)infos);
                break;
            default:
                Debug.LogError("BAD SENSORIAL INFOS RECEIVE ON BRAINFUNCTION (Hear / smell / View).");
                break;
        }
    }

    private float FindClosestObject(Vector3 position, out IdentifiateObject obj, List<IdentifiateObject> ObjectList)
    {
        obj = null;
        float currentDistance = float.MaxValue;
        if (ObjectList.Count > 0)
        {
            float tmpDistance;

            obj = ObjectList[0];
            currentDistance = Vector3.Distance(position, obj.PossibleArea);
            for (int i = 0; i < ObjectList.Count; ++i)
            {
                tmpDistance = Vector3.Distance(position, ObjectList[i].PossibleArea);
                if (tmpDistance < currentDistance)
                {
                    currentDistance = tmpDistance;
                    obj = ObjectList[i];
                }
            }
            return currentDistance;
        }
        return currentDistance;
    }

    public void UpdateSenses()
    {
        List<IdentifiateObject> identifiateObject = new List<IdentifiateObject>();
        IdentifiateObject tmp;
        foreach(HearInfosClass hearInfos in  CurrentHearInfos)
        {
            float distance = FindClosestObject(hearInfos.EmitterPosition, out tmp, identifiateObject);
            if (distance < VectorRadiusAcceptable && tmp != null)
            {
                tmp.HearedInfos.Add(hearInfos);
            }
            else
            {
                tmp = new IdentifiateObject();
                tmp.PossibleArea = hearInfos.EmitterPosition;
                tmp.HearedInfos.Add(hearInfos);
                identifiateObject.Add(tmp);
            }
        }

        foreach (SmellInfosClass smellInfos in CurrentSmellInfos)
        {
            float distance = FindClosestObject(smellInfos.EmitterPosition, out tmp, identifiateObject);
            if (distance < VectorRadiusAcceptable && tmp != null)
            {
                tmp.SmelledInfos.Add(smellInfos);
            }
            else
            {
                tmp = new IdentifiateObject();
                tmp.PossibleArea = smellInfos.EmitterPosition;
                tmp.SmelledInfos.Add(smellInfos);
                identifiateObject.Add(tmp);
            }
        }

        foreach (ViewInfosClass viewInfos in CurrentViewInfos)
        {
            float distance = FindClosestObject(viewInfos.EmitterPosition, out tmp, identifiateObject);
            if (distance < VectorRadiusAcceptable && tmp != null)
            {
                tmp.ViewedInfos.Add(viewInfos);
            }
            else
            {
                tmp = new IdentifiateObject();
                tmp.PossibleArea = viewInfos.EmitterPosition;
                tmp.ViewedInfos.Add(viewInfos);
                identifiateObject.Add(tmp);
            }
        }

        CreatureInstance.CreatureBrain.ReceiveNewInputObject(identifiateObject);
    }

    private void TreatHearingInfos(HearInfosClass hear)
    {
        this.CurrentHearInfos.Add(hear);
    }

    private void TreatSmellingInfos(SmellInfosClass smell)
    {
        this.CurrentSmellInfos.Add(smell);
    }

    private void TreatTastingInfos(TasteInfosClass taste)
    {

    }

    private void TreatTouchingInfos(TouchInfosClass touch)
    {

    }

    private void TreatViewingInfos(ViewInfosClass view)
    {
        this.CurrentViewInfos.Add(view);
    }
}
