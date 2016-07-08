using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerActionManagerScript : MonoBehaviour {

    public  List<ActionClass> ActionList;
    private Dictionary<GameData.Action,ActionClass> ActionListByAction;
    public Text ActionButtonText;

    public Animator ButtonAnimator;

    private int index = 0;

	// Use this for initialization
	public void Initialize () {
        this.ButtonAnimator.SetTrigger("ChangeVisibility");
        this.ActionListByAction = new Dictionary<GameData.Action, ActionClass>();
        foreach (ActionClass act in this.ActionList)
        {
            this.ActionListByAction.Add(act.ActionType, act);
        }
        this.ActionList.Sort(new ActionComparer());
        this.ActionButtonText.text = this.ActionList[index].ActionName;
    }
	
    void OnTriggerEnter(Collider other)
    {
        List<GameData.Action> objActionEnabled = GameData.EnvironnementManagerInstance.GetPossibleActionForCollider(other);
        if(objActionEnabled == null)
        {
            Debug.Log("No action for the object " + other.gameObject.name);
            return;
        }

        foreach(GameData.Action act in objActionEnabled)
        {
            ActionListByAction[act].AddCandidate();
        }
    }

    void OnTriggerExit(Collider other)
    {
        List<GameData.Action> objActionEnabled = GameData.EnvironnementManagerInstance.GetPossibleActionForCollider(other);
        if (objActionEnabled == null)
        {
            Debug.Log("No action for the object " + other.gameObject.name);
            return;
        }

        foreach (GameData.Action act in objActionEnabled)
        {
            ActionListByAction[act].RemoveCandidate();
        }
    }

    bool SetIndexToNextPossibleAction()
    {
        int maxIteration = this.ActionList.Count;
        do
        {
            index = (index + 1) % this.ActionList.Count;
            maxIteration--;
        } while (!this.ActionList[index].IsFeasible && maxIteration > -1);
        return maxIteration > -1;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("ChangeAction"))
        {
            //index = (index+1) % this.ActionList.Count;
            if (SetIndexToNextPossibleAction())
            {
                this.ButtonAnimator.SetTrigger("ChangeVisibility");
                this.ActionButtonText.text = this.ActionList[index].ActionName;
            }
            else
            {
                this.ActionButtonText.text = "Rien";
            }
        }
        if (Input.GetButtonDown("Action1")) //B controller or E
        {
            this.ActionList[index].CheckCanExecute();
        }
    }
}
