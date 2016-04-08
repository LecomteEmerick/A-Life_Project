using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {

    public LoadingScreenScript LoadingScript;

    public UnityEvent StaticDataInitializeEvent;
    public UnityEvent InitializationEvent;

	// Use this for initialization
	void Start () {
        //this.LoadingScript.SetStepNumber(2);
        //this.LoadingScript.StartLoading("Loading static data");
        StaticDataInitializeEvent.Invoke();
        //this.LoadingScript.AddStep("Loading Dynamic data");
        InitializationEvent.Invoke();
        //this.LoadingScript.EndLoading();
    }
}
