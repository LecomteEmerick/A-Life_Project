using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeScript : MonoBehaviour {

    public TreeInfosClass TreeInfos;

    private Vector3 GrowthRate;
    private bool IsMature;

    private List<FruitClass> TreeFruit;

    void Start ()
    {
        //InitializeTree();
    }
	
    public void InitializeTree()
    {
        TreeInfos.transform.localScale = TreeInfos.TreeMinSize;
        this.GrowthRate = (TreeInfos.TreeMaxSize - TreeInfos.TreeMinSize) / TreeInfos.TreeGrowTimeSecond;
        StartCoroutine(GrowTree());
    }

    IEnumerator GrowTree()
    {
        int time = TreeInfos.TreeGrowTimeSecond;
        while (time > 0)
        {
            TreeInfos.transform.localScale += GrowthRate;
            yield return new WaitForSeconds(1.0f);
            --time;
        }
        TreeInfos.transform.localScale = TreeInfos.TreeMaxSize;
        StartCoroutine(LifetimeTree());
    }

    IEnumerator LifetimeTree()
    {
        TreeInfos.LifeTimeSeconds = 0;
        int IntervalCheck = TreeInfos.MaxLifeTimeSeconds / 10;
        while(TreeInfos.IsAlive)
        {
            ++TreeInfos.LifeTimeSeconds;
            yield return new WaitForSeconds(1.0f);
            //Death Condition
            if(TreeInfos.LifeTimeSeconds > TreeInfos.MaxLifeTimeSeconds / 2 && TreeInfos.LifeTimeSeconds % IntervalCheck == 0)
            {
                float value = (float)(TreeInfos.MaxLifeTimeSeconds - TreeInfos.LifeTimeSeconds) / (float)TreeInfos.MaxLifeTimeSeconds;
                float rand = Random.Range(0.0f, 1.0f);
                float val = Mathf.Pow(value, 1.0f/3.0f);
                if ( rand > val)
                {
                    TreeInfos.IsAlive = false;
                }
            }
        }
        StartCoroutine(DeathTree());
    }

    IEnumerator DeathTree()
    {
        int time = TreeInfos.TreeGrowTimeSecond;
        while (time > 0)
        {
            TreeInfos.transform.localScale -= GrowthRate;
            yield return new WaitForSeconds(1.0f);
            --time;
        }
        //object poolling a faire
        //Debug.Log("Object : " + TreeInfos.gameObject.name + " is dead after : " + TreeInfos.LifeTimeSeconds / (TreeInfos.MaxLifeTimeSeconds / 10) + " cycles (RIP)");
        Destroy(TreeInfos.gameObject);
    }
}
