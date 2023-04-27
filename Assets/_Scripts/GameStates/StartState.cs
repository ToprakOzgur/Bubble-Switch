using System;
using UnityEngine;

public class StartState : BaseState
{
    public static event Action<bool> OnGameStarted = delegate { };
    private GameBase currentGame;
    public override void OnActivate()
    {

        OnGameStarted(true);
        //Todo: create game for selected mode
        currentGame = new ArcadeGame();
    }

    public override void OnDeactivate()
    {
        throw new System.NotImplementedException();
    }

    public override void OnUpdate()
    {

    }
}
