using UnityEngine;
using System.Collections;

public class CollisionTestScript : MonoBehaviour {

    [SerializeField]
    private CreatureClass CreatureInfos;


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter : " + name);
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit : " + name);
    }
}
