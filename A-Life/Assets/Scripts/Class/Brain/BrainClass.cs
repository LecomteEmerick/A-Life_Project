using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrainClass : MonoBehaviour {

    public InputSensesScript InputSenses;
    public BrainMemoryClass MemoryInfos;
    public BrainChimicalClass ChimicalInfos;

    private List<IdentifiateObject> TreatIdentifiateObject;

    public void Initialize()
    {
        this.InputSenses.Initialize();
        this.ChimicalInfos.Initialize();
        this.MemoryInfos.Initialize();
        InvokeRepeating("UpdateBrain", GameData.SensesStartDelay, GameData.SensesUpdateDelay);
    }

    public void ReceiveNewInputObject(List<IdentifiateObject> newObject)
    {
        TreatIdentifiateObject = newObject;
    }

    public void UpdateBrain()
    {
        ChimicalInfos.UpdateChimicalProduction();
    }

}
