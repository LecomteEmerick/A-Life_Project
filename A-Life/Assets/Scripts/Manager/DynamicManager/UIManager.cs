using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

    public List<GameObject> DisabledPanel;
    public List<GameObject> EnabledPanel;

    public void Initialize()
    {
        foreach (GameObject obj in this.DisabledPanel)
            obj.SetActive(false);
        foreach (GameObject obj in this.EnabledPanel)
            obj.SetActive(true);
    }

}
