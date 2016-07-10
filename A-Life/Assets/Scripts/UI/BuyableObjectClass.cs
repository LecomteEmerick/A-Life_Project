using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuyableObjectClass : MonoBehaviour {

    public Sprite ObjectImage;
    public string ObjectName;

    public GameData.PoolledObjectType ObjectType;

    public Text Ui_TextField;
    public Image Ui_ImageField;

    public Button Ui_Button;
    public Text Ui_Button_Text;

    public void Initialize()
    {
        this.Ui_TextField.text = ObjectName;
        this.Ui_ImageField.sprite = ObjectImage;
        if(GameData.EnvironnementManagerInstance.HasObjectOnPool(ObjectType))
        {
            this.Ui_Button_Text.text = "Acheter";
            this.Ui_Button.interactable = true;
        }else
        {
            this.Ui_Button_Text.text = "Rupture";
            this.Ui_Button.interactable = false;
        }

    }
}
