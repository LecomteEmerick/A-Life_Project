using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnvironnementManager : MonoBehaviour {

    public List<ObjectClass> SceneObject;
    public List<ObjectClass> SceneFood;

    public Dictionary<Collider, ObjectClass> ObjectList;
    public Dictionary<Collider, ObjectClass> FoodList;

    public void Initialize()
    {
        foreach(ObjectClass obj in SceneObject)
        {
            obj.Initialize();
        }

        this.ObjectList = new Dictionary<Collider, ObjectClass>();
        foreach(ObjectClass obj in SceneObject)
        {
            try
            {
                this.ObjectList.Add(obj.Collider, obj);
            }
            catch
            {
                Debug.Log("Error occured when extracting object pool scene. Check your EnvManager, an object can be placed two time : " + obj.Name);
            }
        }

        foreach (ObjectClass food in SceneFood)
        {
            food.Initialize();
        }

        this.FoodList = new Dictionary<Collider, ObjectClass>();
        foreach (ObjectClass food in SceneFood)
        {
            try
            {
                this.FoodList.Add(food.Collider, food);
            }
            catch
            {
                Debug.Log("Error occured when extracting object pool scene. Check your EnvManager, an object can be placed two time : " + food.Name);
            }
        }
    }

}
