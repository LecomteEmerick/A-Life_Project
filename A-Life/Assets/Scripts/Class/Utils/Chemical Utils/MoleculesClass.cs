using UnityEngine;
using System.Collections;

public class MoleculesClass {

    public readonly MoleculesBankManager.MoleculesSymbols Symbols;
    public readonly int Quantity;

    public MoleculesClass(MoleculesBankManager.MoleculesSymbols symbol, int quantity)
    {
        this.Symbols = symbol;
        this.Quantity = quantity;
    }
}
