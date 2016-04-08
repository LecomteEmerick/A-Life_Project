using UnityEngine;
using System.Collections.Generic;

public class ChimicalComponentClass {

    public readonly string Name;
    public readonly List<MoleculesClass> MoleculesComponent;

    public ChimicalComponentClass(string name, List<MoleculesClass> moleculesComponent)
    {
        this.Name = name;
        this.MoleculesComponent = moleculesComponent;
    }

}
