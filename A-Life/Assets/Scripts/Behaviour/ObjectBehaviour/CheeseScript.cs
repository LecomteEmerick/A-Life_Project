using UnityEngine;
using System.Collections;

public class CheeseScript : MonoBehaviour {

    public SmellEmitter Emitter;
    public Collider CheeseCollider;
    public AudioClip Bite;
    private SmellInfosClass odor;
    private int NumberEat = 3;
    public void Initialize()
    {
        ChimicalComponentClass chimical = GameData.MoleculesBankInstance.GetChemicalComponent(MoleculesBankManager.ChemicalFormula.ButyricAcid);
        odor = new SmellInfosClass(15.0f, chimical);
        Emitter.Initialize();
        Emitter.EmitteContinueOdour(odor);
    }

    public void EatCheese(CreatureClass creature)
    {
        creature.AudioSource.clip = Bite;
        creature.AudioSource.Play();

        this.gameObject.transform.localScale -= new Vector3(0.33f, 0.33f, 0.33f);
        this.NumberEat--;
        if (this.NumberEat < 1)
            GameData.EnvironnementManagerInstance.GetPooledObjectForCollider(this.CheeseCollider).Deactivate();

        creature.CreatureBrain.ChimicalInfos.ChangeValueForChimical(GameData.BrainChimical.Glucose, -2.0f);
    }

}
