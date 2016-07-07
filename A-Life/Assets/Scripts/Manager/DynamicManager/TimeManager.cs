using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour {

    public enum SkyboxType { Night, Normal, Cloudy }

    [SerializeField]
    private SkyboxClass MidnightTexture;

    [SerializeField]
    private SkyboxClass NightTexture;

    [SerializeField]
    private SkyboxClass EarlyMorning;

    [SerializeField]
    private SkyboxClass MorningTexture;

    [SerializeField]
    private SkyboxClass NoonTexture;

    [SerializeField]
    private SkyboxClass AfterNoonTexture;

    [SerializeField]
    private SkyboxClass SunsetTexture;

    [SerializeField]
    private Text Clock;

    [SerializeField]
    private Text PeriodeOfTheDay;

    [SerializeField]
    private Light Sun;

    [SerializeField]
    private Particle RainEmmiter;

    //Value in second who defined the real time for 24h in game (default value 3h : 10 800 seconds)
    // one day 24h = 86400 seconde
    public float DayTimeValue = 10800;
    private float CurrentTime = 0;
    private float InGameSecondToLifeDay;


    private float currentBlend = 0.0f;
    private float blendStep = 0.1f;

    private string[] MaterialsVariableStart = { "_FrontTex", "_BackTex", "_LeftTex", "_RightTex", "_UpTex", "_DownTex" };
    private string[] MaterialsVariableEnd = { "_FrontTex2", "_BackTex2", "_LeftTex2", "_RightTex2", "_UpTex2", "_DownTex2" };

    private SkyboxClass[] SkyboxDayCycle;
    private const int NumberOfCycles=8;

    private int SkyboxDayCycleIndexStart = NumberOfCycles-1;
    private int SkyboxDayCycleIndexEnd = 0;
    void Start()
    {

        //Application.runInBackground = true;
        InGameSecondToLifeDay = 86400 / DayTimeValue;

        blendStep = DayTimeValue / NumberOfCycles;
        SetSunnyDayCycle();

        ChangeMaterialsTexture(true, SkyboxDayCycle[SkyboxDayCycleIndexStart]);
        ChangeMaterialsTexture(false, SkyboxDayCycle[SkyboxDayCycleIndexEnd]);
        SkyboxDayCycleIndexStart = SkyboxDayCycleIndexEnd;
        ++SkyboxDayCycleIndexEnd;

        PeriodeOfTheDay.text = SkyboxDayCycle[SkyboxDayCycleIndexStart].Name + " to " + SkyboxDayCycle[SkyboxDayCycleIndexEnd].Name;
    }

    void Update()
    {
        PrintRealHours();

        CurrentTime = (CurrentTime + Time.deltaTime) % DayTimeValue;
        currentBlend += Time.deltaTime / blendStep;

        //Sun.intensity = CurrentTime / (DayTimeValue/2);

        if (currentBlend >= 1.0f)
        {
            ChangeMaterialsTexture(true, SkyboxDayCycle[SkyboxDayCycleIndexStart]);
            ChangeMaterialsTexture(false, SkyboxDayCycle[SkyboxDayCycleIndexEnd]);

            PeriodeOfTheDay.text = SkyboxDayCycle[SkyboxDayCycleIndexStart].Name + " to " + SkyboxDayCycle[SkyboxDayCycleIndexEnd].Name;

            //Debug.LogError(SkyboxDayCycle[SkyboxDayCycleIndexStart].Name + " to " + SkyboxDayCycle[SkyboxDayCycleIndexEnd].Name + " is set at : " + CurrentTime);

            SkyboxDayCycleIndexStart = SkyboxDayCycleIndexEnd;
            SkyboxDayCycleIndexEnd = (SkyboxDayCycleIndexEnd + 1) % NumberOfCycles;

            currentBlend -= 1.0f;
        }

        RenderSettings.skybox.SetFloat("_Blend", currentBlend);
    }

    void PrintRealHours()
    {
        float realTimeSecond = CurrentTime * InGameSecondToLifeDay;
        int seconds = (int)(realTimeSecond % 60);
        int minutes = (int)((realTimeSecond / 60) % 60);
        int hours = (int)(realTimeSecond / 3600);

        Clock.text = "Current Time : " + hours + "H " + minutes + "M " + seconds + "s";

        //Debug.LogFormat("{0}H {1}M {2}s", hours, minutes, seconds);
    }

    void SetSunnyDayCycle()
    {
        SkyboxDayCycle = new SkyboxClass[NumberOfCycles];
        SkyboxDayCycle[0] = MidnightTexture;
        SkyboxDayCycle[1] = NightTexture;
        SkyboxDayCycle[2] = EarlyMorning;
        SkyboxDayCycle[3] = MorningTexture;
        SkyboxDayCycle[4] = NoonTexture;
        SkyboxDayCycle[5] = AfterNoonTexture;
        SkyboxDayCycle[6] = SunsetTexture;
        //SkyboxDayCycle[7] = EarlyDusk;
        SkyboxDayCycle[7] = NightTexture;
    }

    void ChangeMaterialsTexture(bool isFirst, SkyboxClass NewTexture)
    {
        if (isFirst)
        {
            RenderSettings.skybox.SetTexture(MaterialsVariableStart[0], NewTexture._Front);
            RenderSettings.skybox.SetTexture(MaterialsVariableStart[1], NewTexture._Back);
            RenderSettings.skybox.SetTexture(MaterialsVariableStart[2], NewTexture._Left);
            RenderSettings.skybox.SetTexture(MaterialsVariableStart[3], NewTexture._Right);
            RenderSettings.skybox.SetTexture(MaterialsVariableStart[4], NewTexture._Top);
            RenderSettings.skybox.SetTexture(MaterialsVariableStart[5], NewTexture._Bottom);
        }else
        {
            RenderSettings.skybox.SetTexture(MaterialsVariableEnd[0], NewTexture._Front);
            RenderSettings.skybox.SetTexture(MaterialsVariableEnd[1], NewTexture._Back);
            RenderSettings.skybox.SetTexture(MaterialsVariableEnd[2], NewTexture._Left);
            RenderSettings.skybox.SetTexture(MaterialsVariableEnd[3], NewTexture._Right);
            RenderSettings.skybox.SetTexture(MaterialsVariableEnd[4], NewTexture._Top);
            RenderSettings.skybox.SetTexture(MaterialsVariableEnd[5], NewTexture._Bottom);
        }
    }
}
