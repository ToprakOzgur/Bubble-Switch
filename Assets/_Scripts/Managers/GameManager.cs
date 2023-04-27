using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private BaseState currentState;
    public BaseState CurrentState
    {
        get { return currentState; }
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
}

public enum GameMode
{
    Arcade,
    Zen,

}