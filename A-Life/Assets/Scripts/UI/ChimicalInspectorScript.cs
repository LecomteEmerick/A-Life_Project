using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChimicalInspectorScript : MonoBehaviour {

    private CreatureClass InspectedCreature;

    public Transform PanelTransform;

    public Text CreatureName_TextField;

    public ChimicalDisplayClass Chimical_UI_Adrénaline;
    public ChimicalDisplayClass Chimical_UI_Endorphine;
    public ChimicalDisplayClass Chimical_UI_Glucose;
    public ChimicalDisplayClass Chimical_UI_Sérotonine;
    public ChimicalDisplayClass Chimical_UI_Energie;

    private bool isShow = false;

    void Update()
    {
        if(Input.GetButtonDown("InspectCreature"))
        {
            if (isShow)
                Hide();
            else
            {
                CreatureClass tmp = GameData.CreatureManagerInstance.GetInspectedCreature();
                if (tmp != null)
                    ShowForCreature(tmp);
            }

        }
    }

    public void ChangeInspectedCreature()
    {
        this.InspectedCreature = GameData.CreatureManagerInstance.GetInspectedCreature();
        this.CreatureName_TextField.text = this.InspectedCreature.GetCreatureName();
    }

    public void ShowForCreature(CreatureClass inspectedCreature)
    {
        this.isShow = true;
        PanelTransform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        this.InspectedCreature = inspectedCreature;
        this.CreatureName_TextField.text = this.InspectedCreature.GetCreatureName();
        GameData.CreatureManagerInstance.InterestCreatureChange.AddListener(ChangeInspectedCreature);
        InvokeRepeating("ActualizeChimical", 0.0f, GameData.SensesUpdateDelay);
    }

    public void Hide()
    {
        this.isShow = false;
        PanelTransform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
        GameData.CreatureManagerInstance.InterestCreatureChange.RemoveListener(ChangeInspectedCreature);
        CancelInvoke("ActualizeChimical");
    }

    public void ActualizeChimical()
    {
        Dictionary<GameData.BrainChimical, ChimicalClass> chimicals = InspectedCreature.CreatureBrain.ChimicalInfos.CreatureChimical;

        this.Chimical_UI_Adrénaline.Chimical_TextField.text = chimicals[GameData.BrainChimical.Adrenaline].PlayerDisplayName;
        this.Chimical_UI_Adrénaline.Chimical_SliderField.value = chimicals[GameData.BrainChimical.Adrenaline].Value;

        this.Chimical_UI_Endorphine.Chimical_TextField.text = chimicals[GameData.BrainChimical.Endorphine].PlayerDisplayName;
        this.Chimical_UI_Endorphine.Chimical_SliderField.value = chimicals[GameData.BrainChimical.Endorphine].Value;

        this.Chimical_UI_Glucose.Chimical_TextField.text = chimicals[GameData.BrainChimical.Glucose].PlayerDisplayName;
        this.Chimical_UI_Glucose.Chimical_SliderField.value = chimicals[GameData.BrainChimical.Glucose].Value;

        this.Chimical_UI_Sérotonine.Chimical_TextField.text = chimicals[GameData.BrainChimical.Sérotonine].PlayerDisplayName;
        this.Chimical_UI_Sérotonine.Chimical_SliderField.value = chimicals[GameData.BrainChimical.Sérotonine].Value;

        this.Chimical_UI_Energie.Chimical_TextField.text = chimicals[GameData.BrainChimical.Energie].PlayerDisplayName;
        this.Chimical_UI_Energie.Chimical_SliderField.value = chimicals[GameData.BrainChimical.Energie].Value;
    }

}
