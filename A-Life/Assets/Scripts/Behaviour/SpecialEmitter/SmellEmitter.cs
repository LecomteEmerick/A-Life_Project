using UnityEngine;
using System.Collections.Generic;

public class SmellEmitter : MonoBehaviour {

    public Transform ObjectTransform;

    private List<SmellInfosClass> ContinusSmell;

    public void Initialize()
    {
        this.ContinusSmell = new List<SmellInfosClass>();
    }

    private void EmittePonctualOdour(SmellInfosClass smell)
    {
        //CARE MEMORY USAGE (THREAD?)
        List<CreatureClass> creatureImpacted = GameData.CreatureManagerInstance.SphereCastCreature(ObjectTransform.position, smell.Power);
        foreach (CreatureClass creature in creatureImpacted)
            creature.SmellReceptor.Receptor.Reception(smell, ObjectTransform.position);
    }

    public void EmitteContinueOdour(SmellInfosClass smell)
    {
        ContinusSmell.Add(smell);
    }

    public void RemoveContinueOdour(SmellInfosClass smell)
    {
        ContinusSmell.Remove(smell);
    }

    void FixedUpdate()
    {
        foreach (SmellInfosClass smell in this.ContinusSmell)
        {
            EmittePonctualOdour(smell);
        }
    }
}
