using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionComparer : IComparer<ActionClass>
{
    public int Compare(ActionClass x, ActionClass y)
    {
        return x.Priority - y.Priority;
    }
}

public abstract class ActionClass : MonoBehaviour
{

    public string ActionName;
    public GameData.Action ActionType;

    public int Priority;


    public abstract void Initialize();

    public abstract bool CheckCanExecute();

    public abstract void Execute();




    public virtual bool IsFeasible { get { return this.isFeasible; } }
    private bool isFeasible = false;
    private int numberPossible;
    public void AddCandidate() { this.numberPossible++; isFeasible = true; }
    public void RemoveCandidate() { this.numberPossible--; isFeasible = this.numberPossible > 0; }
}