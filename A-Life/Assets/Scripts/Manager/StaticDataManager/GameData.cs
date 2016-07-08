using UnityEngine;

public static class GameData {

    static public EnvironnementManager EnvironnementManagerInstance;
    static public CreatureManager CreatureManagerInstance;
    static public SoundBankManager SoundBankInstance;
    static public MoleculesBankManager MoleculesBankInstance;
    static public PlayerClass PlayerInstance;

    static public Camera ActiveCamera;

    public enum PoolledObjectType { DragonEgg, AppleTreeSeed, AppleTree, PalmTreeSeed, PalmTree, Apple, Coconuts };

    public enum Action { Catch, Throw, Compliment, Plant, Hurt }
}
