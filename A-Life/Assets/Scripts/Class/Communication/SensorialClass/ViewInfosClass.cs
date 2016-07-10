using UnityEngine;
using System.Collections;

public class ViewInfosClass : SensorialClass {

    public PoolledObjectClass ViewedObject;

    public ViewInfosClass(PoolledObjectClass obj)
    {
        this.ViewedObject = obj;
        base.Type = SensesType.View;
    }

}
