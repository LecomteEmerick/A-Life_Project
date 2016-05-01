using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeScript : MonoBehaviour {

    public TreeInfosClass TreeInfos;
    public float MaxTimeForFruitDetachSeconds = 10.0f;

    private Vector3 GrowthRate;
    private bool IsMature;

    public SoundEmitter Emitter;
    private HearInfosClass Sound;

    void Start ()
    {
        InitializeTree();
    }
	
    public void InitializeTree()
    {
        this.Sound = new HearInfosClass(10.0f, 50.0f, 20.0f, 1.0f);
        TreeInfos.transform.localScale = TreeInfos.TreeMinSize;
        this.GrowthRate = (TreeInfos.TreeMaxSize - TreeInfos.TreeMinSize) / TreeInfos.TreeGrowTimeSecond;
        StartCoroutine(GrowTree());
    }

    IEnumerator GrowTree()
    {
        int time = TreeInfos.TreeGrowTimeSecond;
        while (time > 0)
        {
            TreeInfos.TreeInstance.transform.localScale += GrowthRate;
            yield return new WaitForSeconds(1.0f);
            --time;
        }
        TreeInfos.TreeInstance.transform.localScale = TreeInfos.TreeMaxSize;
        StartCoroutine(LifetimeTree());
    }

    int GetEmptyIndex()
    {
        for(int i=0; i < TreeInfos.FruitAttach.Count; ++i)
        {
            if (!TreeInfos.FruitAttach[i].IsUsed)
                return i;
        }
        return -1;
    }
    IEnumerator LifetimeTree()
    {
        TreeInfos.LifeTimeSeconds = 0;
        int IntervalCheck = TreeInfos.MaxLifeTimeSeconds / 10;
        int index;
        while(TreeInfos.IsAlive)
        {
            //Fruit Generation
            if ((index = GetEmptyIndex()) != -1)
            {
                TreeInfos.FruitAttach[index].IsUsed = true;
                GameObject InstaceFruit = (GameObject)Instantiate(TreeInfos.Fruit, TreeInfos.FruitAttach[index].TransformPosition.position, Quaternion.identity);
                FruitClass InstanciateFruitClass = InstaceFruit.GetComponent<FruitClass>();
                InstanciateFruitClass.Initialize();
                TreeInfos.FruitAttach[index].Fruit = InstanciateFruitClass;
            }

            foreach(AnchorClass Anchor in TreeInfos.FruitAttach)
            {
                if (!Anchor.IsUsed)
                    continue;

                if (!Anchor.Fruit.IsMature)
                {
                    Anchor.Fruit.gameObject.transform.localScale += Anchor.Fruit.GrowthRate;
                    ++Anchor.Fruit.currentGrowthTime;
                    if(Anchor.Fruit.CheckFruitMaturity())
                    {
                        StartCoroutine(DetachFruit(Anchor));
                    }
                }
            }

            //Death Condition
            if (TreeInfos.LifeTimeSeconds > TreeInfos.MaxLifeTimeSeconds / 2 && TreeInfos.LifeTimeSeconds % IntervalCheck == 0)
            {
                float value = (float)(TreeInfos.MaxLifeTimeSeconds - TreeInfos.LifeTimeSeconds) / (float)TreeInfos.MaxLifeTimeSeconds;
                float rand = Random.Range(0.0f, 1.0f);
                float val = Mathf.Pow(value, 1.0f / 3.0f);
                if (rand > val)
                {
                    TreeInfos.IsAlive = false;
                }
            }

            ++TreeInfos.LifeTimeSeconds;
            yield return new WaitForSeconds(1.0f);


        }
        StartCoroutine(DeathTree());
    }

    IEnumerator DetachFruit(AnchorClass Anchor)
    {
        float delayTime = Random.Range(0.0f, MaxTimeForFruitDetachSeconds);
        yield return new WaitForSeconds(delayTime);
        Anchor.IsUsed = false;
        Anchor.Fruit.FruitRigidBody.useGravity = true;
        this.Emitter.EmittePonctualSound(this.Sound);
    }

    IEnumerator DeathTree()
    {
        int time = TreeInfos.TreeGrowTimeSecond;
        while (time > 0)
        {
            TreeInfos.TreeInstance.transform.localScale -= GrowthRate;
            yield return new WaitForSeconds(1.0f);
            --time;
        }
        //object poolling a faire
        Destroy(TreeInfos.TreeInstance);
    }
}
