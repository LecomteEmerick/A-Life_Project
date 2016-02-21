using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmitterClass<T> where T : SensorialClass {

    private T SensorialInfos;
    private List<ReceptorClass<T>> ReceptorList;


    public EmitterClass(T sensorialInfos)
    {
        this.ReceptorList = new List<ReceptorClass<T>>();
        this.SensorialInfos = sensorialInfos;
    }

    public void RegisterReceptor(ReceptorClass<T> Receptor)
    {
        if (!this.ReceptorList.Contains(Receptor))
        {
            this.ReceptorList.Add(Receptor);

            if(GameData.IsLightDebugMode || GameData.IsHardDebugMode)
                Debug.Log("Receptor Register");
        }
    }

    public void Emit()
    {
        foreach(ReceptorClass<T> receptor in this.ReceptorList)
        {
            receptor.Reception(this.SensorialInfos);
        }
    }

    public void UnRegisterReceptor(ReceptorClass<T> Receptor)
    {
        if (this.ReceptorList.Contains(Receptor))
        {
            this.ReceptorList.Remove(Receptor);

            if (GameData.IsLightDebugMode || GameData.IsHardDebugMode)
                Debug.Log("Receptor Unregister");
        }
    }
}
