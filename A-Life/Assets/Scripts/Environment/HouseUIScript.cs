using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HouseUIScript : MonoBehaviour {

    public SelectionPanelScript ChooseOptionPanel;

    public int PlayerLayerNumber = 13;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == PlayerLayerNumber)
        {
            ChooseOptionPanel.Open();
        }
    }
}
