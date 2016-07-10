using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BrainChimicalClass : MonoBehaviour {

    public List<ChimicalClass> Chimicals;

    public Dictionary<GameData.BrainChimical, ChimicalClass> CreatureChimical;

    public void Initialize()
    {
        this.CreatureChimical = new Dictionary<GameData.BrainChimical, ChimicalClass>();
        foreach(ChimicalClass chimical in Chimicals)
        {
            this.CreatureChimical.Add(chimical.ChimicalType, chimical);
            //chimical.Initialize(chimical.Value);
        }
    }

    public void UpdateChimicalProduction()
    {
        foreach (ChimicalClass chimical in Chimicals)
        {
            chimical.UpdateChimical();
        }
    }

    public void ChangeProductionRate(GameData.BrainChimical chimical, float value)
    {
        this.CreatureChimical[chimical].ChangeProductionRate(value);
    }

    public void ChangeEliminationRate(GameData.BrainChimical chimical, float value)
    {
        this.CreatureChimical[chimical].ChangeEliminationRate(value);
    }

}
