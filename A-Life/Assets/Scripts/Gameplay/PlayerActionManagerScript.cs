using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerActionManagerScript : MonoBehaviour {

    public List<ActionClass> ActionList;
    public PlayerClass playerInfos;
    public LayerMask CatchableLayer;

    public AudioClip PlayerRaiseSound;

    public HandClass Hand;

    public Text ActionButtonText;
    public Animator ButtonAnimator;

    private int index = 0;
    private Dictionary<GameData.Action, ActionClass> ActionListByAction;

    // Use this for initialization
    public void Initialize () {
        this.ButtonAnimator.SetTrigger("ChangeVisibility");
        this.ActionListByAction = new Dictionary<GameData.Action, ActionClass>();
        foreach (ActionClass act in this.ActionList)
        {
            this.ActionListByAction.Add(act.ActionType, act);
            act.Initialize();
        }
        this.ActionList.Sort(new ActionComparer());
        if (this.ActionList.Count > 0)
        {
            SetIndexToNextPossibleAction();
            this.ActionButtonText.text = this.ActionList[index].ActionName;
        }
        else
            this.ActionButtonText.text = "Rien";
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
        UpdateActionButton();
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
        UpdateActionButton();
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

    private void UpdateActionButton()
    {
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

    // Update is called once per frame
    void Update()
    {
        if(playerInfos.HasControl)
        {
            if (Input.GetButtonDown("ChangeAction"))
            {
                UpdateActionButton();
            }
            if (Input.GetButtonDown("Action1")) //B controller or E
            {
                if (this.ActionList[index].CheckCanExecute())
                    this.ActionList[index].Execute();
            }
            if (Input.GetButtonDown("Action2")) //X controller or A
            {
                if(!Hand.isFull)
                {
                    Collider target = GetTarget();
                    if (target != null)
                    {
                        PoolledObjectClass obj;
                        if (target.gameObject.layer == 8) //creature layer
                        {
                            obj = GameData.CreatureManagerInstance.GetCreatureByCollider(target).CreaturePoolClass;
                        }else
                        {
                            obj = GameData.EnvironnementManagerInstance.GetPooledObjectForCollider(target);
                        }

                        obj.PoolledObectInstance.transform.parent = Hand.HandPosition;
                        obj.PoolledObjectCollider.enabled = false;
                        if (obj.PoolledObjectRigibody != null)
                        {
                            obj.PoolledObjectRigibody.useGravity = false;
                            obj.PoolledObjectRigibody.isKinematic = true;
                        }
                        target.transform.localPosition = new Vector3(0.0f,0.0f,0.0f);
                        Hand.CarriedObject = obj;
                        Hand.isFull = true;

                        playerInfos.PlayerAudioSource.clip = PlayerRaiseSound;
                        playerInfos.PlayerAudioSource.Play();
                    }
                }
                else
                {
                    Hand.CarriedObject.transform.parent = null;
                    Hand.CarriedObject.PoolledObjectCollider.enabled = true;
                    if (Hand.CarriedObject.PoolledObjectRigibody != null)
                    {
                        Hand.CarriedObject.PoolledObjectRigibody.useGravity = true;
                        Hand.CarriedObject.PoolledObjectRigibody.isKinematic = false;
                    }
                    Hand.isFull = false;
                    if (playerInfos.EntityRigidBody.velocity.magnitude > 0.5f)
                    {
                        Hand.CarriedObject.PoolledObjectRigibody.velocity = playerInfos.EntityRigidBody.velocity * 2.0f;
                        Hand.CarriedObject.PoolledObjectRigibody.AddForce(Vector3.up * 50.0f);
                    }
                }
            }
        }
    }

    Collider GetTarget()
    {
        RaycastHit hit;
        Vector3 startRay = playerInfos.transform.position;
        startRay.y += playerInfos.EntityTransform.localScale.y;

        //Debug.DrawRay(startRay, GameData.ActiveCamera.transform.forward * 7.5f, Color.red, 9999999);
        if (Physics.Raycast(startRay, GameData.ActiveCamera.transform.forward , out hit, 7.5f, CatchableLayer.value))
        {
            return hit.collider;
        }
        return null;
    }
}
