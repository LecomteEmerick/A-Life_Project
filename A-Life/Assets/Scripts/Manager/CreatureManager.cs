using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreatureManager : MonoBehaviour {

    public List<CreatureClass> SceneCreatureList;

    public Dictionary<Collider, CreatureClass> CreatureList;


	// Use this for initialization
	void Start ()
    {
        this.CreatureList = new Dictionary<Collider, CreatureClass>();
        foreach(CreatureClass creatureInfos in SceneCreatureList)
        {
            this.CreatureList.Add(creatureInfos.Collider, creatureInfos);
        }
	}
}
