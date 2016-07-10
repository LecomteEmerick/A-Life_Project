using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class FruitGrowScript : MonoBehaviour {

    public TreeInfosClass TreeInfos;
    public float MaxTimeForFruitDetachSeconds = 10.0f;

    public void StartFruitGrowth()
    {
        StartCoroutine(FruitGrowthLife());
    }

    IEnumerator FruitGrowthLife()
    {
        int index;
        while (TreeInfos.IsAlive)
        {
            if ((index = GetEmptyIndex()) != -1)
            {
                PoolledObjectClass pooledObject = GameData.EnvironnementManagerInstance.GetObjectFromPool(TreeInfos.Fruit);//(GameObject)Instantiate(TreeInfos.Fruit, TreeInfos.FruitAttach[index].TransformPosition.position, Quaternion.identity);
                if (pooledObject != null)
                {
                    TreeInfos.FruitAttach[index].IsUsed = true;
                    pooledObject.Activate(TreeInfos.FruitAttach[index].TransformPosition.position);
                    pooledObject.PoolledObjectRigibody.useGravity = false;
                    pooledObject.PoolledObjectCollider.enabled = false;
                    FruitClass InstanciateFruitClass = pooledObject.PoolledObectInstance.GetComponent<FruitClass>();
                    InstanciateFruitClass.Initialize();
                    TreeInfos.FruitAttach[index].Fruit = InstanciateFruitClass;
                }
            }

            foreach (AnchorClass Anchor in TreeInfos.FruitAttach)
            {
                if (!Anchor.IsUsed)
                    continue;

                if (!Anchor.Fruit.IsMature)
                {
                    Anchor.Fruit.gameObject.transform.localScale += Anchor.Fruit.GrowthRate;
                    ++Anchor.Fruit.currentGrowthTime;
                    if (Anchor.Fruit.CheckFruitMaturity())
                    {
                        StartCoroutine(DetachFruit(Anchor));
                    }
                }
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator DetachFruit(AnchorClass Anchor)
    {
        float delayTime = Random.Range(0.0f, MaxTimeForFruitDetachSeconds);
        yield return new WaitForSeconds(delayTime);
        Anchor.IsUsed = false;
        Anchor.Fruit.FruitRigidBody.useGravity = true;
        Anchor.Fruit.FruitCollider.enabled = true;
        //this.Emitter.EmittePonctualSound(this.Sound);
    }

    int GetEmptyIndex()
    {
        for (int i = 0; i < TreeInfos.FruitAttach.Count; ++i)
        {
            if (!TreeInfos.FruitAttach[i].IsUsed)
                return i;
        }
        return -1;
    }
}
