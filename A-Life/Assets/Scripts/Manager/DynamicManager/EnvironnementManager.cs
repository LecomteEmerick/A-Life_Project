using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnvironnementManager : MonoBehaviour {

    public List<PoolledObjectClass> PooledObject;

    public Dictionary<Collider, PoolledObjectClass> ObjectListByCollider;
    public Dictionary<GameData.PoolledObjectType, List<PoolledObjectClass>> ObjectListByType;

    public List<TreeInfosClass> TreesList;
    public void Initialize()
    {
        this.ObjectListByCollider = new Dictionary<Collider, PoolledObjectClass>();
        this.ObjectListByType = new Dictionary<GameData.PoolledObjectType, List<PoolledObjectClass>>();
        foreach (PoolledObjectClass obj in PooledObject)
        {
            try
            {
                this.ObjectListByCollider.Add(obj.PoolledObjectCollider, obj);
                if (this.ObjectListByType.ContainsKey(obj.ObjectType))
                    this.ObjectListByType[obj.ObjectType].Add(obj);
                else
                {
                    this.ObjectListByType.Add(obj.ObjectType, new List<PoolledObjectClass>());
                    this.ObjectListByType[obj.ObjectType].Add(obj);
                }

                if (obj.PoolledObectInstance.activeSelf)
                    obj.StartEvent.Invoke();
            }
            catch
            {
                Debug.Log("Error occured when extracting object pool scene. Check your EnvManager, an object can be placed two time : " + obj.PoolledObectInstance.name);
            }
        }

        foreach(TreeInfosClass tree in TreesList)
        {
            if(tree.isEternal)
            {
                tree.FruitGrowth.StartFruitGrowth();
            }
        }
    }

    public bool HasObjectOnPool(GameData.PoolledObjectType type)
    {
        int index = 0;
        if (!this.ObjectListByType.ContainsKey(type))
            return false;

        List<PoolledObjectClass> foodList = this.ObjectListByType[type];
        while (index < foodList.Count)
        {
            if (!foodList[index].PoolledObectInstance.activeSelf)
            {
                return true;
            }
            index++;
        }
        return false;
    }

    public PoolledObjectClass GetObjectFromPool(GameData.PoolledObjectType type)
    {
        int index = 0;
        if (!this.ObjectListByType.ContainsKey(type))
            return null;

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

    public PoolledObjectClass GetPooledObjectForCollider(Collider col)
    {
        if (this.ObjectListByCollider.ContainsKey(col))
        {
            return this.ObjectListByCollider[col];
        }
        return null;
    }
} 
