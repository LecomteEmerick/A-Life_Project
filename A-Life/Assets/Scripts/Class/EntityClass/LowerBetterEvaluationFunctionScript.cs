using UnityEngine;
using System.Collections;
using System;

public class LowerBetterEvaluationFunctionScript : EvalutionFunctionClass {

    public override float Evaluate(ChimicalClass chimical)
    {
        return chimical.Value;
    }
}
