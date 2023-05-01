using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action<Game> OnGameCreated = delegate { };
    public static event Action OnGameLost = delegate { };
    public GameSettings gameSettings;
    public AdventureData adventureData;

    [HideInInspector]
    public Game currentGame;


    public void StartArcadeGame()
    {
        var vTubes = FindObjectsOfType<VTube>();
        currentGame = new Game(vTubes);
        OnGameCreated(currentGame);

    }

    public void StartZenGame()
    {
        var vTubes = FindObjectsOfType<VTube>();
        currentGame = new ZenGame(vTubes);
        OnGameCreated(currentGame);
    }

    public void StartAdventureGame()
    {
        var vTubes = FindObjectsOfType<VTube>();
        currentGame = new AdventureGame(vTubes);
        OnGameCreated(currentGame);
    }

    public IEnumerator DelayExecuter(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }

}