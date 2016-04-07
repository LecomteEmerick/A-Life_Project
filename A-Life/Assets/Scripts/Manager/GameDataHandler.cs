using UnityEngine;
using System.Collections;

public class GameDataHandler : MonoBehaviour {

    [SerializeField]
    private EnvironnementManager envrironnementManager;

    [SerializeField]
    private CreatureManager creatureManager;

    [SerializeField]
    private SoundBankManager soundBankManager;

    public void Initialize () {
        GameData.EnvironnementManagerInstance = this.envrironnementManager;
        GameData.CreatureManagerInstance = this.creatureManager;
        GameData.SoundBankInstance = this.soundBankManager;
	}
}
