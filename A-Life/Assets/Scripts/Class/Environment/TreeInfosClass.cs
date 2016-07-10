using UnityEngine;
using System.Collections.Generic;

public class TreeInfosClass : MonoBehaviour {
    public GameObject TreeInstance;
    public TreeScript TreeScript;
    public FruitGrowScript FruitGrowth;

    //Growth infos
    public Vector3 TreeMinSize = new Vector3(0.1f, 0.1f, 0.1f);
    public Vector3 TreeMaxSize = new Vector3(1.0f, 1.0f, 1.0f);
    public int TreeGrowTimeSecond;

    //Fruit infos
    public List<AnchorClass> FruitAttach;
    public GameData.PoolledObjectType Fruit;

    //Life Infos
    public bool IsAlive = true;
    public int MaxLifeTimeSeconds = 36000;
    public int LifeTimeSeconds;

    public bool isEternal;

}
