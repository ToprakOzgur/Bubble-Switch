using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverState : BaseState
{
    public static event Action OnGameLost = delegate { };
    public override void OnActivate()
    {
        OnGameLost();
    }

    public override void OnDeactivate()
    {

    }

    public override void OnUpdate()
    {

    }
}
