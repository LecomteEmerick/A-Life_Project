using UnityEngine;
using System.Collections.Generic;
using System;

public class PlantScript : ActionClass
{
    public HandClass PlayerHand;
    public LayerMask plantableLayer;
    public List<GameData.PoolledObjectType> plantTypeAccepted;

    public Vector3 PlantPosition;

    public override void Initialize()
    {
        
    }

    public override bool CheckCanExecute()
    {
        bool playerReady = PlayerHand.isFull && plantTypeAccepted.Contains(PlayerHand.CarriedObject.ObjectType);

        if (!playerReady)
            return false;

        RaycastHit hit;
        Vector3 startRay = PlayerHand.transform.position;

        Debug.DrawRay(startRay, GameData.ActiveCamera.transform.forward * 7.5f, Color.red, 9999999);
        if (Physics.Raycast(startRay, GameData.ActiveCamera.transform.forward, out hit, 7.5f, plantableLayer.value))//terrain layer
        {
            if (hit.normal == Vector3.up)
            {
                PlantPosition = hit.point;
                return true;
            }
        }
        return false;
    }

    public override void Execute()
    {
        PlayerHand.CarriedObject.transform.parent = null;
        PlayerHand.CarriedObject.transform.position = PlantPosition;
        PlayerHand.isFull = false;

        PlayerHand.CarriedObject.PlayerInteractionEvent.Invoke();
    }
}
