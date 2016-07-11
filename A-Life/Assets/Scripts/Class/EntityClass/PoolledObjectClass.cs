using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class PoolledObjectClass : MonoBehaviour {

    public GameObject PoolledObectInstance;
    public Rigidbody PoolledObjectRigibody;
    public Collider PoolledObjectCollider;

    public List<GameData.Action> InteractionPossible;

    public GameData.PoolledObjectType ObjectType;

    public UnityEvent StartEvent;
    public UnityEvent PlayerInteractionEvent;
    public CreatureUnityEventClass CreatureInteractionEvent;

    public void Activate(Vector3 SpawnPosition)
    {
        this.PoolledObectInstance.SetActive(true);
        this.PoolledObectInstance.transform.position = SpawnPosition;
        if (PoolledObjectRigibody != null)
        {
            this.PoolledObjectRigibody.useGravity = true;
            this.PoolledObjectRigibody.isKinematic = false;
        }
        this.PoolledObjectCollider.enabled = true;
        StartEvent.Invoke();
    }

    public void Deactivate()
    {
        this.PoolledObjectRigibody.velocity = Vector3.zero;
        this.PoolledObectInstance.transform.position = Vector3.zero;
        if (PoolledObjectRigibody != null)
        {
            this.PoolledObjectRigibody.useGravity = false;
            this.PoolledObjectRigibody.isKinematic = true;
        }
        this.PoolledObjectCollider.enabled = false;
        this.PoolledObectInstance.SetActive(false);
    }
}
