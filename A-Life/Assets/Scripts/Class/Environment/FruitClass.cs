using UnityEngine;
using System.Collections;

public class FruitClass : MonoBehaviour {

    public string Name;

    //Growth infos
    public int growingTimeSeconds;
    public int currentGrowthTime;
    public Vector3 GrowthRate { get; private set; }

    public Vector3 FruitMinSize = new Vector3(1.0f, 1.0f, 1.0f);
    public Vector3 FruitMaxSize = new Vector3(2.0f, 2.0f, 2.0f);

    //Logical
    public Rigidbody FruitRigidBody;


    public Vector3 AnchorPosition;

    public bool IsMature { get; private set; }

    public void Initialize()
    {
        this.IsMature = false;
        this.currentGrowthTime = 0;
        this.GrowthRate = (FruitMaxSize - FruitMinSize) / growingTimeSeconds;
        this.gameObject.transform.localScale = FruitMinSize;

    }

    public bool CheckFruitMaturity()
    {
        if(currentGrowthTime >= growingTimeSeconds)
        {
            this.IsMature = true;
            this.gameObject.transform.localScale = this.FruitMaxSize;
            return true;
        }
        return false;
    }
}
