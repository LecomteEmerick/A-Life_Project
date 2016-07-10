using UnityEngine;
using System.Collections;

public class HearInfosClass : SensorialClass
{
    public float Volume;

    public float Frequency;

    public HearInfosClass()
    {
        base.Type = SensesType.Hearing;
    }

    public HearInfosClass( float Volume, float frequency)
    {
        this.Volume = Volume;
        this.Frequency = frequency;
        base.Type = SensesType.Hearing;
    }

    public HearInfosClass(HearInfosClass infosClass)
    {
        this.Volume = infosClass.Volume;
        this.Frequency = infosClass.Frequency;
        base.Type = SensesType.Hearing;
    }

    public string printInfos()
    {
        return "frequency : " + Frequency + 
            "Volume : " + Volume + " db";
    }
}
