using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [HideInInspector]
    public GameColors ballColor;
    public Color[] colors;

    [HideInInspector] public IBallState containerState;
    [HideInInspector] public IBallState inHTubeState;
    [HideInInspector] public IBallState inVTubeState;
    [HideInInspector] public IBallState switchTubeState;
    [HideInInspector] public IBallState tubeSelectState;
    [HideInInspector] public IBallState spawnState;



    private IBallState currentState;
    public IBallState CurrentState
    {
        get { return currentState; }
        set
        {
            currentState?.OnDeactivate();
            currentState = value;
            currentState?.OnActivate();
        }
    }

    private void Awake()
    {
        containerState = new ContainerState(this);
        inHTubeState = new InHTubeState(this);
        inVTubeState = new InVTubeState(this);
        switchTubeState = new SwitchTubesState(this);
        tubeSelectState = new TubeSelectState(this);
        spawnState = new SpawnState(this);
    }
    public void Spawn()
    {
        CurrentState = spawnState;
    }
    private void Update()
    {
        CurrentState.OnUpdate();
    }

    public void SetColor(GameColors ballColor)
    {
        this.ballColor = ballColor;
        GetComponent<SpriteRenderer>().color = colors[(int)ballColor];
    }

}


