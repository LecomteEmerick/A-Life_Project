using UnityEngine;
using System.Collections.Generic;

public class MoleculesBankManager : MonoBehaviour {

    public enum MoleculesSymbols {
        H, He,
        Li, Be,
        B, C, N, O, F, Ne, Na, Mg,
        Al, Si, P, S, Cl, Ar, K, Ca,
        Sc, Ti, V, Cr, Mn, Fe, Co, Ni, Cu, Zn, Ga, Ge, As, Se, Br, Kr, Rb, Sr,
        Y,  Zr, Nb, Mo, Tc, Ru, Rh, Pd, Ag, Cd, In, Sn, Sb, Te, I,  Xe, Cs, Ba,
        La, Ce, Pr, Nd, Pm, Sm, Eu, Gd, Tb, Dy, Ho, Er, Tm, Yb, Lu, Hf, Ta, W, Re, Os, Ir, Pt, Au, Hg, Tl, Pb, Bi, Po, At, Rn, Fr, Ra,
        Ac, Th, Pa, U, Np, Pu, Am, Cm, Bk, Cf, Es, Fm, Md, No, Lr, Rf, Db, Sg, Bh, Hs, Mt, Ds, Rg, Cn
    }

    public enum ChemicalFormula
    {
        CitricAcid, ButyricAcid
    }

    private Dictionary<string, MoleculesClass> MoleculesList;
    private const int MoleculeCount = 8;

    private Dictionary<ChemicalFormula, ChimicalComponentClass> ChimicalComponentList;
    private const int ChimicalCount = 2;

    public void Initialize()
    {
        this.MoleculesList = new Dictionary<string, MoleculesClass>(MoleculeCount);
        this.MoleculesList.Add("C6", new MoleculesClass(MoleculesSymbols.C, 6));
        this.MoleculesList.Add("H8", new MoleculesClass(MoleculesSymbols.H, 8));
        this.MoleculesList.Add("O7", new MoleculesClass(MoleculesSymbols.O, 7));

        this.MoleculesList.Add("C3", new MoleculesClass(MoleculesSymbols.C, 3));
        this.MoleculesList.Add("H7", new MoleculesClass(MoleculesSymbols.H, 7));
        this.MoleculesList.Add("C", new MoleculesClass(MoleculesSymbols.C, 1));
        this.MoleculesList.Add("O", new MoleculesClass(MoleculesSymbols.O, 1));
        this.MoleculesList.Add("H", new MoleculesClass(MoleculesSymbols.O, 1));

        List<MoleculesClass> tmpMoleculeList = new List<MoleculesClass>();

        this.ChimicalComponentList = new Dictionary<ChemicalFormula, ChimicalComponentClass>(ChimicalCount);
        //Orange ?
        SetMoleculeToList("C6-H8-O7", ref tmpMoleculeList);
        this.ChimicalComponentList.Add(ChemicalFormula.CitricAcid, new ChimicalComponentClass(ChemicalFormula.CitricAcid.ToString(), tmpMoleculeList));
        //Cheese
        SetMoleculeToList("C3-H7-C-O-O-H", ref tmpMoleculeList);
        this.ChimicalComponentList.Add(ChemicalFormula.ButyricAcid, new ChimicalComponentClass(ChemicalFormula.ButyricAcid.ToString(), tmpMoleculeList));
    }

    private void SetMoleculeToList(string MoleculeList, ref List<MoleculesClass> tmpMoleculeList)
    {
        string[] molecules = MoleculeList.Split('-');
        tmpMoleculeList.Clear();
        foreach(string molName in molecules)
        {
            tmpMoleculeList.Add(this.MoleculesList[molName]);
        }
    }

    public ChimicalComponentClass GetChemicalComponent(ChemicalFormula chemical)
    {
        if(this.ChimicalComponentList.ContainsKey(chemical))
        {
            return this.ChimicalComponentList[chemical];
        }
        Debug.LogError("Chemical not found on MoleculesBankManager.");
        return null;
    }
}
