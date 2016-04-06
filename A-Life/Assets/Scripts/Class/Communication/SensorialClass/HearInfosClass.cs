using UnityEngine;
using System.Collections;

public class HearInfosClass : SensorialClass
{
    public float Volume;

    private float LowFrequencyValue;
    private float MediumFrequencyValue;
    private float HighFrequencyValue;

    public HearInfosClass()
    {
        base.Type = SensesType.Hearing;
    }

    public HearInfosClass(float Volume, float LowFrequency, float MediumFrequency, float HighFrequency)
    {
        this.Volume = Volume;
        this.LowFrequencyValue = LowFrequency;
        this.MediumFrequencyValue = MediumFrequency;
        this.HighFrequencyValue = HighFrequency;
        base.Type = SensesType.Hearing;
    }

    public string printInfos()
    {
        return "LowFrequency : " + LowFrequencyValue +
            "MediumFrequency : " + MediumFrequencyValue +
            "HightFrequency : " + HighFrequencyValue +
            "Volume : " + Volume + " db";
    }
}
