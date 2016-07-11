using UnityEngine;
using System.Collections.Generic;

public class ChimicalComponentClass {

    public readonly string Name;
    public readonly List<MoleculesClass> MoleculesComponent;
    public readonly Color MoleculesVisualColor;
    public ChimicalComponentClass(string name, List<MoleculesClass> moleculesComponent, Color color)
    {
        this.Name = name;
        this.MoleculesComponent = moleculesComponent;
        this.MoleculesVisualColor = color;
    }

}
