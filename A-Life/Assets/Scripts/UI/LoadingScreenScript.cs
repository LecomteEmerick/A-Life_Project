using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingScreenScript : MonoBehaviour {

    public Canvas LoadingCanvas;

    public Text LoadingMessage;
    public Slider ProgressBar;

    private int StepNumber;
    private int CurrentStep;

    private float StepValue;

    public void SetStepNumber(int Number)
    {
        this.StepNumber = Number;
        this.StepValue = 1 / Number;
        this.CurrentStep = 0;
    }

    public void StartLoading(string LoadingMessage)
    {
        this.LoadingCanvas.gameObject.SetActive(true);
        this.SetCurrentStep(LoadingMessage, 0);
    }

    public void AddStep(string LoadingMessage)
    {
        ++this.CurrentStep;
        this.ProgressBar.value += this.StepValue;
        this.LoadingMessage.text = LoadingMessage;
    }

    public void SetCurrentStep(string LoadingMessage, int newCurrentStep)
    {
        this.CurrentStep = newCurrentStep;
        this.ProgressBar.value = this.StepValue * this.CurrentStep;
        this.LoadingMessage.text = LoadingMessage;
    }

    public void EndLoading()
    {
        this.SetCurrentStep("Finalisation", this.StepNumber);
        this.LoadingCanvas.gameObject.SetActive(false);
    }

}
