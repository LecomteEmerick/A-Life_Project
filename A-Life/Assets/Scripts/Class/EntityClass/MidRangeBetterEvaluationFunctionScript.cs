using UnityEngine;
using System.Collections;
using System;

public class MidRangeBetterEvaluationFunctionScript : EvalutionFunctionClass
{
    public override float Evaluate(ChimicalClass chimical)
    {
        float x = chimical.Value - ((chimical.UpperDangerValue - chimical.MinValue) / 2);
        return (x * x * x) / 9; 
    }
}
