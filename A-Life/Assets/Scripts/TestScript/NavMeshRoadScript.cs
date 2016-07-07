using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavMeshRoadScript : MonoBehaviour {

    public NavMeshAgent Agent;
    public List<GameObject> CheckPoint;

    int Index = 0;
    List<Vector3> CheckPointPosition;

    public float pathEndThreshold = 0.1f;
    private bool hasPath = false;
    bool AtEndOfPath()
    {
        hasPath |= Agent.hasPath;
        if (hasPath && Agent.remainingDistance <= Agent.stoppingDistance + pathEndThreshold )
            {
            hasPath = false;
            return true;
        }

        return false;
    }


    void Start()
    {
        this.CheckPointPosition = new List<Vector3>();
        foreach(GameObject check in CheckPoint)
        {
            this.CheckPointPosition.Add(check.transform.position);
        }

        Agent.SetDestination(this.CheckPointPosition[Index]);
        Index = Index + 1 % this.CheckPointPosition.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (AtEndOfPath())
        {
            Agent.SetDestination(this.CheckPointPosition[Index]);
            Index = (Index + 1) % this.CheckPointPosition.Count;
        }
    }
}
