using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SelectionPanelScript : MonoBehaviour {

    public AudioSource GeneralAudioSource;
    public AudioClip OpenAudioClip;
    public AudioClip CloseAudioClip;

    public GameObject SelectionPanel;
    public Animator AnimatorPanel;

    public ShopPanelScript ShopPanel;
    public DoctorPanelScript DoctorPanel;

    public Button DefaultSelectedButton;

    public void Open()
    {
        GameData.PlayerInstance.HasControl = false;
        GeneralAudioSource.clip = OpenAudioClip;
        GeneralAudioSource.Play(0);
        this.SelectionPanel.SetActive(true);
        this.DefaultSelectedButton.Select();
        this.AnimatorPanel.SetTrigger("OpenPanel");
    }

    public void Show()
    {
        this.SelectionPanel.SetActive(true);
        this.DefaultSelectedButton.Select();
        this.AnimatorPanel.SetTrigger("OpenPanel");
    }

    //public void EndOpenTransition()
    //{
    //    this.SelectionPanel.transform.rotation = Quaternion.identity;
    //}

    public void OpenShopPanel()
    {
        this.AnimatorPanel.SetTrigger("HidePanel");
        StartCoroutine(DelayedOpenShopPanel());
    }

    IEnumerator DelayedOpenShopPanel()
    {
        yield return new WaitForSeconds(1.05f);
        this.SelectionPanel.SetActive(false);
        ShopPanel.Open();
    }

    public void Close()
    {
        this.AnimatorPanel.SetTrigger("ClosePanel");
        GeneralAudioSource.clip = CloseAudioClip;
        GeneralAudioSource.Play(0);
    }

    public void EndPanelTransition()
    {
        this.SelectionPanel.SetActive(false);
        GameData.PlayerInstance.HasControl = true;
    }
}
