using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ViewReceptor : MonoBehaviour {

    public InputSensesScript BrainHandler;
    public Collider ViewCollider;

    public Dictionary<Collider,ViewInfosClass> ContinusView;
    public void Initialize()
    {
        this.ContinusView = new Dictionary<Collider, ViewInfosClass>();
        this.ViewCollider.enabled = true;
        InvokeRepeating("SendToBrain", GameData.SensesStartDelay, GameData.SensesUpdateDelay);
    }

    void OnTriggerEnter(Collider other)
    {
        PoolledObjectClass obj = GameData.EnvironnementManagerInstance.GetPooledObjectForCollider(other);
        if(obj != null)
        {
            this.ContinusView.Add(other, new ViewInfosClass(obj));
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (this.ContinusView.ContainsKey(other))
            this.ContinusView.Remove(other);
    }

    void SendToBrain()
    {
        foreach(KeyValuePair<Collider, ViewInfosClass> view in this.ContinusView)
        {
            view.Value.EmitterPosition = view.Value.ViewedObject.transform.position;
            BrainHandler.SensorialInformationReceiver(view.Value);
        }
    }


}
