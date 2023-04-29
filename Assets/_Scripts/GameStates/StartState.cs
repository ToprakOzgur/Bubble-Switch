using System;
using UnityEngine;

public class StartState : BaseState
{
    public static event Action<GameBase> OnGameCreated = delegate { };
    public override void OnActivate()
    {
        //Todo: create game for selected mode
        var vTubes = FindObjectsOfType<VTube>();
        var currentGame = new ArcadeGame(vTubes);

        OnGameCreated(currentGame);
    }

    public override void OnDeactivate()
    {

    }

    public override void OnUpdate()
    {

    }
}
