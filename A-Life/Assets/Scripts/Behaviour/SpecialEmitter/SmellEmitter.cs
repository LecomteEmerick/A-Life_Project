using UnityEngine;
using System.Collections.Generic;

public class SmellEmitter : MonoBehaviour {

    public Transform ObjectTransform;

    private List<SmellInfosClass> ContinusSmell;

    public void Initialize()
    {
        this.ContinusSmell = new List<SmellInfosClass>();
        InvokeRepeating("SendToBrain", GameData.SensesStartDelay, GameData.SensesUpdateDelay);
    }

    private void EmittePonctualOdour(SmellInfosClass smell)
    {
        //CARE MEMORY USAGE (THREAD?)
        smell.EmitterPosition = ObjectTransform.position;
        List<CreatureClass> creatureImpacted = GameData.CreatureManagerInstance.SphereCastCreature(ObjectTransform.position, smell.Power);
        foreach (CreatureClass creature in creatureImpacted)
            creature.SmellReceptor.Receptor.Reception(smell);
    }

    public void EmitteContinueOdour(SmellInfosClass smell)
    {
        ContinusSmell.Add(smell);
    }

    public void RemoveContinueOdour(SmellInfosClass smell)
    {
        ContinusSmell.Remove(smell);
    }

    void SendToBrain()
    {
        foreach (SmellInfosClass smell in this.ContinusSmell)
        {
            EmittePonctualOdour(smell);
        }
    }
}
