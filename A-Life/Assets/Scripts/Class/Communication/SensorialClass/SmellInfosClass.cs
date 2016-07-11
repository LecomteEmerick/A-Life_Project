using UnityEngine;
using System.Collections.Generic;

public class SmellInfosClass : SensorialClass {

    public float Power;
    public ChimicalComponentClass ChemicalComponent;
    public SmellInfosClass()
    {
        base.Type = SensesType.Smell;
    }

    public SmellInfosClass(float power, ChimicalComponentClass chemicalComponent)
    {
        this.Power = power;
        this.ChemicalComponent = chemicalComponent;
        base.Type = SensesType.Smell;
    }

    public string printInfos()
    {
       return "Power : " + Power + "\nIs composed of : " + ChemicalComponent;
    }
}
