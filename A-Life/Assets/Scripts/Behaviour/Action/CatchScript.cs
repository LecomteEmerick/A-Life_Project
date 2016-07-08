using UnityEngine;
using System.Collections.Generic;
public class CatchScript : ActionClass
{
    public override bool IsFeasible { get { return true; } }

    public List<int> AcceptedLayer;
    private GameObject Target;

    public override bool CheckCanExecute()
    {
        RaycastHit hit;
        Vector3 centerWorldPos = GameData.ActiveCamera.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, GameData.ActiveCamera.nearClipPlane));
        if(Physics.Raycast(centerWorldPos, GameData.ActiveCamera.transform.forward, out hit))
        {
            Debug.DrawRay(centerWorldPos, GameData.ActiveCamera.transform.forward * 100, Color.red, 9999);
            if (AcceptedLayer.Contains(hit.collider.gameObject.layer))
            {
                Target = hit.collider.gameObject;
                return true;
            }

        }
        return false;
    }

    public override void Execute()
    {
        
    }
}
