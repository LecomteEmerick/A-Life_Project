using UnityEngine;
using System.Collections;

public class FruitClass : MonoBehaviour {

    public GameData.PoolledObjectType FruitType;

    //Growth infos
    public int growingTimeSeconds;
    public int currentGrowthTime;
    public Vector3 GrowthRate { get; private set; }

    public Vector3 FruitMinSize = new Vector3(1.0f, 1.0f, 1.0f);
    public Vector3 FruitMaxSize = new Vector3(2.0f, 2.0f, 2.0f);

    //Logical
    public Rigidbody FruitRigidBody;
    public Collider FruitCollider;


    public SmellEmitter Emitter;

    public Vector3 AnchorPosition;

    public AudioClip Bite;

    private bool isInit = false;
    private int NumberEat = 3;
    private Vector3 EatDecrease;

    public bool IsMature { get; private set; }

    public void Initialize()
    {
        this.IsMature = false;
        this.currentGrowthTime = 0;
        this.GrowthRate = (FruitMaxSize - FruitMinSize) / growingTimeSeconds;
        this.gameObject.transform.localScale = FruitMinSize;

    }

    public void InitializeOnStart()
    {
        this.NumberEat = 3;
        this.EatDecrease = (FruitMaxSize - FruitMinSize) / NumberEat;
        SmellInfosClass odor;
        if (FruitType == GameData.PoolledObjectType.Apple)
            odor = new SmellInfosClass(15.0f, GameData.MoleculesBankInstance.GetChemicalComponent(MoleculesBankManager.ChemicalFormula.ChlorogenicAcid));
        else
            odor = new SmellInfosClass(15.0f, GameData.MoleculesBankInstance.GetChemicalComponent(MoleculesBankManager.ChemicalFormula.CaprylicAcid));
        this.Emitter.Initialize();
        this.Emitter.EmitteContinueOdour(odor);
    }

    public bool CheckFruitMaturity()
    {
        if(currentGrowthTime >= growingTimeSeconds)
        {
            this.IsMature = true;
            this.gameObject.transform.localScale = this.FruitMaxSize;
            InvokeRepeating("EmitSmell", GameData.SensesStartDelay, GameData.SensesUpdateDelay);
            return true;
        }
        return false;
    }

    public void EmitSmell()
    {

    }

    public void EatFruit(CreatureClass creature)
    {
        //if(!isInit)
        //{
        //    this.NumberEat = 3;
        //    this.EatDecrease = (FruitMaxSize - FruitMinSize) / NumberEat;
        //    this.isInit = true;
        //}

        creature.AudioSource.clip = Bite;
        creature.AudioSource.Play();

        this.gameObject.transform.localScale -= this.EatDecrease;
        this.NumberEat--;
        if (this.NumberEat < 1)
            GameData.EnvironnementManagerInstance.GetPooledObjectForCollider(this.FruitCollider).Deactivate();

        creature.CreatureBrain.ChimicalInfos.ChangeValueForChimical(GameData.BrainChimical.Glucose, -2.0f);
    }
}
