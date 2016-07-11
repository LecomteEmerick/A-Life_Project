using UnityEngine;
using System.Collections;

public class GameDataHandler : MonoBehaviour {

    [SerializeField]
    private EnvironnementManager envrironnementManager;

    [SerializeField]
    private CreatureManager creatureManager;

    [SerializeField]
    private SoundBankManager soundBankManager;

    [SerializeField]
    private MoleculesBankManager moleculesBankManager;

    [SerializeField]
    private PlayerClass playerClass;

    [SerializeField]
    private Camera playerCamera;

    public void Initialize () {
        Application.runInBackground = true;
        GameData.EnvironnementManagerInstance = this.envrironnementManager;
        GameData.CreatureManagerInstance = this.creatureManager;
        GameData.SoundBankInstance = this.soundBankManager;
        GameData.MoleculesBankInstance = this.moleculesBankManager;
        GameData.PlayerInstance = this.playerClass;
        GameData.ActiveCamera = this.playerCamera;
    }

    void Update()
    {
        if(Input.GetButtonDown("debugMode"))
        {
            GameData.ShowVisualDebug = !GameData.ShowVisualDebug;
        }
    }
}
