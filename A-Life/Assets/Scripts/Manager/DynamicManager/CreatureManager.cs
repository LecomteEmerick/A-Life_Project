using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class CreatureManager : MonoBehaviour {

    public List<CreatureClass> SceneCreatureList;

    public Dictionary<Collider, CreatureClass> CreatureList;

    public GameObject MarkerInterest;
    public GameObject MarkerCreature;

    public UnityEvent InterestCreatureChange;

    private int index = 0;
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
                this.CreatureList.Add(creatureInfos.EntityCollider, creatureInfos);
            }
            catch
            {
                Debug.Log("Error occured when extracting creature pool scene. Check your EnvManager, an creature can be placed two time : " + creatureInfos.CreatureName);
            }
        }
	}

    public void RegisterCreature(CreatureClass creatureInfos)
    {
        SceneCreatureList.Add(creatureInfos);
        CreatureList.Add(creatureInfos.EntityCollider, creatureInfos);
    }

    public CreatureClass GetCreatureByCollider(Collider col)
    {
        if (CreatureList.ContainsKey(col))
            return CreatureList[col];
        return null;
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

    void Update()
    {
        if (CreatureList.Count > 0)
        {
            if (Input.GetButtonDown("Next Creature"))
            {
                index = (index + 1) % SceneCreatureList.Count;
                MarkerCreature.transform.parent = SceneCreatureList[index].EntityTransform;
                MarkerCreature.transform.localPosition = new Vector3(0.0f,1.0f,0.0f);
                SceneCreatureList[index].CreatureBrain.ObjectOfInterestChange.AddListener(InterestMarkerChange);
                InterestMarkerChange();
                if (InterestCreatureChange != null)
                    InterestCreatureChange.Invoke();
            }
            if (Input.GetButtonDown("Previous Creature"))
            {
                index = (index - 1) % SceneCreatureList.Count;
                MarkerCreature.transform.parent = SceneCreatureList[index].EntityTransform;
                MarkerCreature.transform.localPosition = new Vector3(0.0f, 1.0f, 0.0f);
                SceneCreatureList[index].CreatureBrain.ObjectOfInterestChange.AddListener(InterestMarkerChange);
                InterestMarkerChange();
                if (InterestCreatureChange != null)
                    InterestCreatureChange.Invoke();
            }
        }
    }

    public CreatureClass GetInspectedCreature()
    {
        if(CreatureList.Count > 0)
            return SceneCreatureList[index];
        return null;
    }

    public void InterestMarkerChange()
    {
        SceneCreatureList[index].CreatureBrain.SetInterestMarker(MarkerInterest);
    }
}
