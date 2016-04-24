using UnityEngine;
using System.Collections.Generic;

public class TreeInfosClass : MonoBehaviour {

    public string TreeName;
    public GameObject GameObjectInstance;

    //Growth infos
    public Vector3 TreeMinSize = new Vector3(1.0f, 1.0f, 1.0f);
    public Vector3 TreeMaxSize = new Vector3(2.0f, 2.0f, 2.0f);
    public int TreeGrowTimeSecond;

    //Fruit infos
    public List<GameObject> FruitAttach;
    public FruitClass Fruit;

    //Life Infos
    public bool IsAlive = true;
    public int MaxLifeTimeSeconds = 36000;
    public int LifeTimeSeconds;

}
