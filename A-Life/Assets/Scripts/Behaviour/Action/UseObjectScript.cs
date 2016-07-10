using UnityEngine;
using System.Collections;
using System;

public class UseObjectScript : ActionClass
{
    public PlayerClass playerInfos;
    public LayerMask HittableLayer;

    private Collider Target;

    public override void Initialize()
    {
        
    }

    public override bool CheckCanExecute()
    {
        RaycastHit hit;
        Vector3 startRay = playerInfos.EntityTransform.position;
        startRay.y += playerInfos.EntityTransform.localScale.y;

        //Debug.DrawRay(startRay, GameData.ActiveCamera.transform.forward * 7.5f, Color.red, 9999999);
        if (Physics.Raycast(startRay, GameData.ActiveCamera.transform.forward, out hit, 7.5f, HittableLayer)) //creature layer
        {
            Target = hit.collider;
            return true;
        }
        return false;
    }

    public override void Execute()
    {
        PoolledObjectClass obj = GameData.EnvironnementManagerInstance.GetPooledObjectForCollider(Target);
        if(obj != null)
        {
            obj.PlayerInteractionEvent.Invoke();
        }
    }


}
