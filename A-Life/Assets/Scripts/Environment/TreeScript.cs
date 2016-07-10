using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeScript : MonoBehaviour {

    public TreeInfosClass TreeInfos;

    private Vector3 GrowthRate;

    public FruitGrowScript FruitGrowthScript;

    public SoundEmitter Emitter;
    private HearInfosClass Sound;



    //void Start ()
    //{
    //    InitializeTree();
    //}
	
    public void InitializeTree()
    {
        this.Sound = new HearInfosClass(10.0f, 100.0f);
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

    IEnumerator LifetimeTree()
    {
        TreeInfos.LifeTimeSeconds = 0;
        int IntervalCheck = TreeInfos.MaxLifeTimeSeconds / 10;
        while(TreeInfos.IsAlive)
        {

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
