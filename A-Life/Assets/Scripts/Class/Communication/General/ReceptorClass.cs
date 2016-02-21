using UnityEngine;
using System.Collections;

public class ReceptorClass<T> where T : SensorialClass{

    public delegate void UpdateSenses(T sensorialInfos);
    public UpdateSenses UpdateFunction;

    public ReceptorClass()
    {

    }

    public void Reception(T SensorialInfos)
    {
        UpdateFunction(SensorialInfos);
    }
}
