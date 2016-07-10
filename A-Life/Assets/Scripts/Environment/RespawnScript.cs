using UnityEngine;
using System.Collections;

public class RespawnScript : MonoBehaviour {

    public Transform RespawnPosition;

    void OnTriggerEnter(Collider Other)
    {
        Other.gameObject.transform.position = RespawnPosition.transform.position;
    }

}
