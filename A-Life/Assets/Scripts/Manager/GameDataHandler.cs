using UnityEngine;
using System.Collections;

public class GameDataHandler : MonoBehaviour {

    [SerializeField]
    private EnvironnementManager envrironnementManager;


    [SerializeField]
    private CreatureManager creatureManager;

	void Start () {
        GameData.EnvironnementManagerInstance = this.envrironnementManager;
        GameData.CreatureManagerInstance = this.creatureManager;
	}
}
