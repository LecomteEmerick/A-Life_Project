using UnityEngine;
using System.Collections;

public class FruitClass : MonoBehaviour {

    public string Name;

    public int growingTimeSeconds;
    public int currentGrowthTime;

    public Vector3 FruitMinSize = new Vector3(1.0f, 1.0f, 1.0f);
    public Vector3 FruitMaxSize = new Vector3(2.0f, 2.0f, 2.0f);

    public GameObject Prefab;


}
