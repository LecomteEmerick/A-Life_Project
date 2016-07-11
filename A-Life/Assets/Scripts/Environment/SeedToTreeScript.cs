using UnityEngine;
using System.Collections;

public class SeedToTreeScript : MonoBehaviour {

    public PoolledObjectClass SeedObject;
    public GameData.PoolledObjectType TreeType;

    public Renderer SeedRenderer;

    private float SecondBeforeRemove = 3.0f;

    public void TransformToTree()
    {
        PoolledObjectClass obj = GameData.EnvironnementManagerInstance.GetObjectFromPool(TreeType);
        if(obj == null)
        {
            SeedRenderer.material.color = Color.black;
        }else
        {
            if(Random.Range(0, 4) == 0)
            {
                SeedRenderer.material.color = Color.black;
            }else
            {
                StartCoroutine(RemoveSeed(obj));
                return;
            }
        }
        StartCoroutine(RemoveSeed(null));
    }

    public IEnumerator RemoveSeed(PoolledObjectClass obj)
    {
        yield return new WaitForSeconds(SecondBeforeRemove);
        if(obj != null)
        {
            obj.Activate(this.SeedObject.PoolledObectInstance.transform.position);
            //obj.StartEvent.Invoke();
        }
        this.SeedObject.Deactivate();
    }

}
