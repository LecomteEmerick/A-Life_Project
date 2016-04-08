using UnityEngine;
using System.Collections;

public class ReceptorClass<T> where T : SensorialClass{

    public delegate void UpdateSenses(T sensorialInfos);
    public UpdateSenses UpdateFunction;

    public delegate void UpdateSensesWithPosition(T sensorialInfos, Vector3 EmittorPosition);
    public UpdateSensesWithPosition UpdateEmittorFunction;

    public ReceptorClass()
    {

    }

    public void Reception(T SensorialInfos)
    {
        UpdateFunction(SensorialInfos);
    }

    public void Reception(T SensorialInfos, Vector3 EmittorPosition)
    {
        UpdateEmittorFunction(SensorialInfos, EmittorPosition);
    }
}
