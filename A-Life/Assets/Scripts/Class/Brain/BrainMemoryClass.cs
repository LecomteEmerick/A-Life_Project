using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChimicalModifier
{
    public GameData.BrainChimical Chimical;
    public float ProductionModifer;
    public float EliminationModifier;
    public float RawValueModifier;

    public ChimicalModifier(GameData.BrainChimical chimical, float productionModifier, float eliminationModifier, float rawValueModifier)
    {
        this.Chimical = chimical;
        this.EliminationModifier = eliminationModifier;
        this.ProductionModifer = productionModifier;
        this.RawValueModifier = rawValueModifier;
    }
}

public class ObjectModifier
{
    public GameData.PoolledObjectType Object;
    public float ProductionModifer;
    public float EliminationModifier;
    public float RawValueModifier;

    public ObjectModifier(GameData.PoolledObjectType obj, float productionModifier, float eliminationModifier, float rawValueModifier)
    {
        this.Object = obj;
        this.EliminationModifier = eliminationModifier;
        this.ProductionModifer = productionModifier;
        this.RawValueModifier = rawValueModifier;
    }
}

public class BrainMemoryClass : MonoBehaviour {

    public Dictionary<GameData.PoolledObjectType, List<ChimicalModifier>> InteractionModifierByObject;
    public Dictionary<GameData.BrainChimical, List<ObjectModifier>> InteractionModifierByChimical;

    public void Initialize()
    {
        this.InteractionModifierByChimical = new Dictionary<GameData.BrainChimical, List<ObjectModifier>>();
        this.InteractionModifierByObject = new Dictionary<GameData.PoolledObjectType, List<ChimicalModifier>>();
    }

    public void MemoryzeInteraction(GameData.PoolledObjectType obj, List<ChimicalModifier> modifiers)
    {
        if (!this.InteractionModifierByObject.ContainsKey(obj))
        {
            this.InteractionModifierByObject.Add(obj, new List<ChimicalModifier>());
        }

        foreach (ChimicalModifier chimicalInfos in modifiers)
        {
            this.InteractionModifierByObject[obj].Add(chimicalInfos);

            if (!this.InteractionModifierByChimical.ContainsKey(chimicalInfos.Chimical))
            {
                this.InteractionModifierByChimical.Add(chimicalInfos.Chimical, new List<ObjectModifier>());
            }
            this.InteractionModifierByChimical[chimicalInfos.Chimical].Add( new ObjectModifier(obj, chimicalInfos.ProductionModifer, chimicalInfos.EliminationModifier, chimicalInfos.RawValueModifier));
        }
    }

    public void MemorizeObject(GameData.PoolledObjectType obj, List<HearInfosClass> heared, List<SmellInfosClass> smelled, ViewInfosClass viewed)
    {

    }

    public void GetBestObjectForChimical(GameData.BrainChimical Chimical)
    {

    }

}
