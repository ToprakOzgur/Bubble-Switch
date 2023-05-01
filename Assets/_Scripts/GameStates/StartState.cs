using System;
using UnityEngine;

public class StartState : BaseState
{
    public static event Action<Game> OnGameCreated = delegate { };
    public override void OnActivate()
    {
        //Todo: create game for selected mode
        var vTubes = FindObjectsOfType<VTube>();
        var currentGame = new Game(vTubes);
        // var currentGame = new ZenGame(vTubes);
        // var currentGame = new AdventureGame(vTubes);

        OnGameCreated(currentGame);
        // Managers.Spawner.StartSpawn();
    }

    public override void OnDeactivate()
    {

    }

    public override void OnUpdate()
    {

    }
}
