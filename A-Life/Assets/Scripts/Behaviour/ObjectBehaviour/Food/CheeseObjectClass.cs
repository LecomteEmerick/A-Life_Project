using UnityEngine;
using System.Collections;

public class CheeseObjectClass : ObjectClass {

    public SmellEmitter smellDispatcher;

    private SmellInfosClass smellInfos;

    public override void Initialize()
    {
        smellDispatcher.Initialize();
        smellInfos = new SmellInfosClass(5.0f, GameData.MoleculesBankInstance.GetChemicalComponent(MoleculesBankManager.ChemicalFormula.ButyricAcid));
        smellDispatcher.EmitteContinueOdour(smellInfos);
    }

}
