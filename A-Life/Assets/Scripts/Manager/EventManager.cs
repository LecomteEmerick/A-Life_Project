using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {

    public UnityEvent StaticDataInitializeEvent;
    public UnityEvent InitializationEvent;

	// Use this for initialization
	void Start () {
        StaticDataInitializeEvent.Invoke();
        InitializationEvent.Invoke();
    }
}
