using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnvironnementManager : MonoBehaviour {

    public List<ObjectClass> SceneObject;

    public Dictionary<Collider, ObjectClass> ObjectList;

    void Start()
    {
        this.ObjectList = new Dictionary<Collider, ObjectClass>();
        foreach(ObjectClass obj in SceneObject)
        {
            this.ObjectList.Add(obj.Collider, obj);
        }
    }

}
