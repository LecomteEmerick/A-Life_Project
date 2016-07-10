using UnityEngine;

public static class GameData {

    static public EnvironnementManager EnvironnementManagerInstance;
    static public CreatureManager CreatureManagerInstance;
    static public SoundBankManager SoundBankInstance;
    static public MoleculesBankManager MoleculesBankInstance;
    static public PlayerClass PlayerInstance;

    static public Camera ActiveCamera;

    public enum PoolledObjectType { DragonEgg, AppleTreeSeed, AppleTree, PalmTreeSeed, PalmTree, Apple, Coconuts, Creature, Ball };

    public enum Action { Compliment, Plant, Hurt , Whistle, UseObject}

    public enum BrainChimical { Adrenaline, Endorphine, Glucose, Sérotonine, Testostérone, Oestrogene }

    public enum EntityType {  Player, Creature }

    static public float SensesStartDelay = 1.0f;
    static public float SensesUpdateDelay = 1.0f;
}
