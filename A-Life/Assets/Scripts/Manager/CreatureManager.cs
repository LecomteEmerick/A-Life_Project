using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatureManager : MonoBehaviour {

    public List<CreatureClass> SceneCreatureList;

    public Dictionary<Collider, CreatureClass> CreatureList;


	// Use this for initialization
	public void Intialize()
    {
        //foreach(CreatureClass creature in SceneCreatureList)
        //{
        //    creature.Initialize();
        //}

        this.CreatureList = new Dictionary<Collider, CreatureClass>();
        foreach(CreatureClass creatureInfos in SceneCreatureList)
        {
            this.CreatureList.Add(creatureInfos.Collider, creatureInfos);
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
