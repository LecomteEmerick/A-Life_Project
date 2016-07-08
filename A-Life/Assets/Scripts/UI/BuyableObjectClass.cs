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
    }
}
