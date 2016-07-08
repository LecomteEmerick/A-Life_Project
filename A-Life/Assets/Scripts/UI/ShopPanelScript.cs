using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;

public class ShopPanelScript : MonoBehaviour {

    public GameObject ShopPanel;
    public Button ExitButton;
    public Animator ShopAnimator;

    public AudioSource GeneralAudioSource;
    public AudioClip buyAudioClip;

    public List<BuyableObjectClass> objectList;
    public UnityEvent CloseEvent;

    public void Open()
    {
        this.ShopPanel.SetActive(true);
        this.ShopAnimator.SetTrigger("OpenPanel");
        foreach (BuyableObjectClass obj in objectList)
        {
            obj.Initialize();
        }
        this.ExitButton.Select();

    }

    public void Close()
    {
        this.ShopAnimator.SetTrigger("ClosePanel");
    }

    public void EndCloseAnimation()
    {
        this.ShopPanel.SetActive(false);
        CloseEvent.Invoke();
    }

    public void OnBuyEvent(BuyableObjectClass objectClass)
    {
        GeneralAudioSource.clip = buyAudioClip;
        GeneralAudioSource.Play();

        Vector3 direction = new Vector3(Random.Range(-100.0f,100.0f), 100.0f, Random.Range(-100.0f, 100.0f));

        PoolledObjectClass obj = GameData.EnvironnementManagerInstance.GetObjectFromPool(objectClass.ObjectType);

        if (obj != null)
        {
            obj.Activate(GameData.PlayerInstance.PlayerTransform.position + (Vector3.up * 3));
            obj.PoolledObjectRigibody.AddForce(direction);
        }
        else
        {
            objectClass.Ui_Button_Text.text = "Rupture";
            objectClass.Ui_Button.interactable = false;
        }
    }

}
