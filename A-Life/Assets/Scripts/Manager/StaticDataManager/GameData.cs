using UnityEngine;

public static class GameData {

    static public EnvironnementManager EnvironnementManagerInstance;
    static public CreatureManager CreatureManagerInstance;
    static public SoundBankManager SoundBankInstance;
    static public MoleculesBankManager MoleculesBankInstance;
    static public PlayerClass PlayerInstance;

    static public Camera ActiveCamera;

    public enum PoolledObjectType { DragonEgg, AppleTreeSeed, AppleTree, PalmTreeSeed, PalmTree, Apple, Coconuts, Creature, Ball, Cheese };

    public enum Action { Compliment, Plant, Hurt , Whistle, UseObject}

    public enum BrainChimical { Adrenaline, Endorphine, Glucose, Sérotonine, Testostérone, Oestrogene, Energie }

    public enum EntityType {  Player, Creature }

    static public float SensesStartDelay = 1.0f;
    static public float SensesUpdateDelay = 1.0f;

    static public bool ShowVisualDebug = false;

    static public bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 5.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }
}
