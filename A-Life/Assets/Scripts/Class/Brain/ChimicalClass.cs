using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ConnexeChimical
{
    public ChimicalClass ChimicalInfos;
    public float RawValueConnexeValue;
    public float ProductionConnexeValue;
    public float EliminationConnexeValue;
}

public class ChimicalClass : MonoBehaviour
{
    public string PlayerDisplayName;
    public GameData.BrainChimical ChimicalType;

    public float Value;

    public float ProductionRateVariable;
    private float ProductionRateBase=1.0f;

    public float EliminationRateVariable;
    private float EliminationRateBase=1.0f;

    public float UpperDangerValue;
    public float MinValue;

    public EvalutionFunctionClass EvaluationFunction;

    public List<ConnexeChimical> ImpactedChimical;

    //public void Initialize()
    //{
    //    this.Value = (UpperDangerValue + MinValue) / 2;
    //    this.EliminationRateVariable = 0.0f;
    //    this.ProductionRateBase = 0.0f;
    //}

    //public void Initialize(float productionRate, float eleminationRate)
    //{
    //    this.Value = (UpperDangerValue + MinValue) / 2;
    //    this.EliminationRateVariable = eleminationRate;
    //    this.ProductionRateVariable = productionRate;
    //}

    public void Initialize(float value, float productionRate, float eleminationRate)
    {
        this.Value = value;
        this.EliminationRateVariable = eleminationRate;
        this.ProductionRateVariable = productionRate;
    }

    public void UpdateChimical()
    {
        this.Value += ProductionRateBase + ProductionRateVariable;
        this.Value -= EliminationRateBase + EliminationRateVariable;
        if (this.Value < MinValue)
            this.Value = MinValue;
    }

    public void ChangeRawValue(float value)
    {
        Value += value;
        foreach (ConnexeChimical chimical in this.ImpactedChimical)
        {
            chimical.ChimicalInfos.ChangeRawValue(value * chimical.RawValueConnexeValue);
        }
    }

    public void ChangeProductionRate(float value)
    {
        ProductionRateVariable += value;
        foreach(ConnexeChimical chimical in this.ImpactedChimical)
        {
            chimical.ChimicalInfos.ChangeProductionRate(value * chimical.ProductionConnexeValue);
        }
    }

    public void ChangeEliminationRate(float value)
    {
        EliminationRateVariable += value;
        foreach (ConnexeChimical chimical in this.ImpactedChimical)
        {
            chimical.ChimicalInfos.ChangeEliminationRate(value * chimical.EliminationConnexeValue);
        }
    }

    public float EvaluateImpact()
    {
        return EvaluationFunction.Evaluate(this);
    }
}
