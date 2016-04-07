using UnityEngine;
using System.Collections.Generic;

public class BallObjectClass : ObjectClass {

    public BallScript ballScript;

    public override void Initialize()
    {
        ballScript.Initialize();
    }
}
