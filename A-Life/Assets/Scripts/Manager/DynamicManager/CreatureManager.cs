﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatureManager : MonoBehaviour {

    public List<CreatureClass> SceneCreatureList;

    public Dictionary<Collider, CreatureClass> CreatureList;


	// Use this for initialization
	public void Intialize()
    {
        foreach (CreatureClass creature in SceneCreatureList)
        {
            creature.Initialize();
        }

        this.CreatureList = new Dictionary<Collider, CreatureClass>();
        foreach(CreatureClass creatureInfos in SceneCreatureList)
        {
            try
            { 
                this.CreatureList.Add(creatureInfos.Collider, creatureInfos);
            }
            catch
            {
                Debug.Log("Error occured when extracting creature pool scene. Check your EnvManager, an creature can be placed two time : " + creatureInfos.CreatureName);
            }
        }
	}

    public List<CreatureClass> SphereCastCreature(Vector3 Position, float Radius)
    {
        List<CreatureClass> creatureCastList = new List<CreatureClass>();
        foreach (CreatureClass creatureInfos in SceneCreatureList)
        {
            if (Vector3.Distance(Position, creatureInfos.transform.position) <= Radius)
                creatureCastList.Add(creatureInfos);
        }
        return creatureCastList;
    }
}