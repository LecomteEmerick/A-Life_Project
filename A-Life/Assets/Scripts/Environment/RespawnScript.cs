using UnityEngine;
using System.Collections;

public class RespawnScript : MonoBehaviour {

    public Transform RespawnPosition;

    void OnTriggerEnter(Collider Other)
    {
        //Other.gameObject.GetComponent<Rigidbody>().velocity.Set(0.0f, 0.0f, 0.0f);
        Other.gameObject.transform.position = RespawnPosition.transform.position;
    }

}
