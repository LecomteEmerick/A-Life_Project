using UnityEngine;
using System.Collections;

public enum SensesType { Hearing, View, Touch, Taste, Smell}

public abstract class SensorialClass {
    public Vector3 EmitterPosition;
    public SensesType Type;

}
