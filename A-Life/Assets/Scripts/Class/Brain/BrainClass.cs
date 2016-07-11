using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class InterestObject
{
    public Vector3 Position;
    public PoolledObjectClass Object;
    public float InterestRate;

    public List<HearInfosClass> HearInfos;
    public List<SmellInfosClass> SmellInfos;
    public ViewInfosClass ViewInfos;

    public InterestObject(float rate)
    {
        this.InterestRate = rate;
    }

    public InterestObject(Vector3 position, ViewInfosClass viewInfos, int rate, List<HearInfosClass> hearInfos, List<SmellInfosClass> smellInfos)
    {
        this.Position = position;
        this.InterestRate = rate;
        this.ViewInfos = viewInfos;
        this.HearInfos = hearInfos;
        this.SmellInfos = smellInfos;

        if (viewInfos != null)
            this.Object = viewInfos.ViewedObject;
    }
}

public class BrainClass : MonoBehaviour {

    public CreatureClass CreatureInfos;

    public InputSensesScript InputSenses;
    public BrainMemoryClass MemoryInfos;
    public BrainChimicalClass ChimicalInfos;

    public InterestObject SelectedInterestingObject;

    public UnityEvent ObjectOfInterestChange;

    private List<IdentifiateObject> TreatIdentifiateObject;

    public void SetInterestMarker(GameObject interestMarker)
    {
        interestMarker.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        interestMarker.transform.rotation = Quaternion.Euler(90.0f,0.0f,0.0f);
        if (SelectedInterestingObject == null)
        {
            interestMarker.transform.parent = null;
            interestMarker.transform.position = Vector3.zero;
            return;
        }

        if (SelectedInterestingObject.Object != null)
        {
            interestMarker.transform.parent = SelectedInterestingObject.Object.transform;
            interestMarker.transform.position = Vector3.up * 2.0f;
        }
        else
        {
            interestMarker.transform.parent = null;
            interestMarker.transform.position = SelectedInterestingObject.Position + Vector3.up * 2.0f;
        }
    }

    public void Initialize()
    {
        this.InputSenses.Initialize();
        this.ChimicalInfos.Initialize();
        this.MemoryInfos.Initialize();
        InvokeRepeating("UpdateBrain", GameData.SensesStartDelay, GameData.SensesUpdateDelay);
    }

    public void ReceiveNewInputObject(List<IdentifiateObject> newObject)
    {
        TreatIdentifiateObject = newObject;
        if (this.SelectedInterestingObject != null)
        {
            foreach (IdentifiateObject obj in TreatIdentifiateObject)
            {
                if (Vector3.Distance(obj.PossibleArea, this.SelectedInterestingObject.Position) < 2.0f)
                {
                    if (obj.ViewedInfos != null)
                    {
                        this.SelectedInterestingObject.Object = obj.ViewedInfos.ViewedObject;
                        this.ObjectOfInterestChange.Invoke();
                    }
                    this.SelectedInterestingObject.HearInfos.AddRange(obj.HearedInfos);
                    this.SelectedInterestingObject.SmellInfos.AddRange(obj.SmelledInfos);
                }
            }
        }
    }

    private void ComputeObjectOfInterest()
    {
        float bestInterestRate = -1;
        List<InterestObject> mostInterestingObject = new List<InterestObject>();
        foreach(IdentifiateObject obj in TreatIdentifiateObject)
        {
            if(obj.GetInterestRate() > bestInterestRate)
            {
                mostInterestingObject.Clear();
                mostInterestingObject.Add(new InterestObject(obj.PossibleArea, obj.ViewedInfos, obj.GetInterestRate(), obj.HearedInfos, obj.SmelledInfos));
                bestInterestRate = obj.GetInterestRate();
            }else if(obj.GetInterestRate() == bestInterestRate)
            {
                mostInterestingObject.Add(new InterestObject(obj.PossibleArea, obj.ViewedInfos, obj.GetInterestRate(), obj.HearedInfos, obj.SmelledInfos));
            }
        }

        if (SelectedInterestingObject != null)
        {
            if (SelectedInterestingObject.InterestRate <= bestInterestRate)
                mostInterestingObject.Add(SelectedInterestingObject);
            else
                return;
        }

        InterestObject randomposition = new InterestObject(bestInterestRate);
        GameData.RandomPoint(CreatureInfos.EntityTransform.position, 30.0f, out randomposition.Position);
        mostInterestingObject.Add(randomposition);

        int selectedObject = Random.Range(0, mostInterestingObject.Count);
        this.SelectedInterestingObject = mostInterestingObject[selectedObject];

        CreatureInfos.CreatureNavMesh.SetDestination(this.SelectedInterestingObject.Position);
        this.SelectedInterestingObject.InterestRate += 5.0f;
        if(ObjectOfInterestChange != null)
            ObjectOfInterestChange.Invoke();
    }

    public void UpdateBrain()
    {
        ChimicalInfos.UpdateChimicalProduction();
        ComputeObjectOfInterest();
        if (SelectedInterestingObject != null)
        {
            SelectedInterestingObject.InterestRate -= 0.5f;
            if (Vector3.Distance(CreatureInfos.EntityTransform.position, SelectedInterestingObject.Position) < 2.0f)
            {
                if(SelectedInterestingObject.Object != null)
                    SelectedInterestingObject.Object.CreatureInteractionEvent.Invoke(CreatureInfos);
                SelectedInterestingObject.InterestRate -= 10.0f;
            }
        }
    }

}
