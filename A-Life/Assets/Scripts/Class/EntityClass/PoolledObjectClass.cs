using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolledObjectClass : MonoBehaviour {

    public GameObject PoolledObectInstance;
    public Rigidbody PoolledObjectRigibody;
    public Collider PoolledObjectCollider;

    public List<GameData.Action> InteractionPossible;

    public GameData.PoolledObjectType ObjectType;

    public void Activate(Vector3 SpawnPosition)
    {
        this.PoolledObectInstance.SetActive(true);
        this.PoolledObectInstance.transform.position = SpawnPosition;
    }

    public void Deactivate()
    {
        this.PoolledObjectRigibody.velocity = Vector3.zero;
        this.PoolledObectInstance.SetActive(false);
    }
}
