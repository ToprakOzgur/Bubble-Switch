using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameSettings gameSettings;
    public AdventureData adventureData;

    [HideInInspector]
    public Game currentGame;
    private BaseState currentState;

    public BaseState CurrentState
    {
        get { return currentState; }
    }

    private void OnEnable()
    {
        StartState.OnGameCreated += GameCreated;
    }


    private void OnDisable()
    {
        StartState.OnGameCreated -= GameCreated;
    }

    //Changes the current game state
    public void SetState(System.Type newStateType)
    {
        currentState?.OnDeactivate();
        currentState = GetComponentInChildren(newStateType) as BaseState;
        currentState?.OnActivate();

    }
    private void Start()
    {
        SetState(typeof(StartState));
    }
    void Update()
    {
        currentState?.OnUpdate();
    }

    private void GameCreated(Game game)
    {
        currentGame = game;
        SetState(typeof(GamePlayState));
    }

    public IEnumerator DelayExecuter(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action();
    }

}