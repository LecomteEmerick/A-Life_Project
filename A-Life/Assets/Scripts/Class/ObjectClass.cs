using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class ObjectClass : MonoBehaviour {

    public string Name;
    public Collider Collider;

    public abstract void Initialize();
}
