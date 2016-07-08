using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnvironnementManager : MonoBehaviour {

    public List<PoolledObjectClass> PooledObject;

    public Dictionary<Collider, PoolledObjectClass> ObjectListByCollider;
    public Dictionary<GameData.PoolledObjectType, List<PoolledObjectClass>> ObjectListByType;

    public void Initialize()
    {
        this.ObjectListByCollider = new Dictionary<Collider, PoolledObjectClass>();
        this.ObjectListByType = new Dictionary<GameData.PoolledObjectType, List<PoolledObjectClass>>();
        foreach (PoolledObjectClass food in PooledObject)
        {
            try
            {
                this.ObjectListByCollider.Add(food.PoolledObjectCollider, food);
                if (this.ObjectListByType.ContainsKey(food.ObjectType))
                    this.ObjectListByType[food.ObjectType].Add(food);
                else
                {
                    this.ObjectListByType.Add(food.ObjectType, new List<PoolledObjectClass>());
                    this.ObjectListByType[food.ObjectType].Add(food);
                }
            }
            catch
            {
                Debug.Log("Error occured when extracting object pool scene. Check your EnvManager, an object can be placed two time : " + food.PoolledObectInstance.name);
            }
        }
    }

    public PoolledObjectClass GetObjectFromPool(GameData.PoolledObjectType type)
    {
        int index = 0;
        List<PoolledObjectClass> foodList = this.ObjectListByType[type];
        while(index < foodList.Count)
        {
            if(!foodList[index].PoolledObectInstance.activeSelf)
            {
                return foodList[index];
            }
            index++;
        }
        return null;
    }

    public List<GameData.Action> GetPossibleActionForCollider(Collider col)
    {
        if(this.ObjectListByCollider.ContainsKey(col))
        {
            return this.ObjectListByCollider[col].InteractionPossible;
        }
        return null;
    }
} 
