using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnState : IBallState
{
    private readonly Ball ball;
    public SpawnState(Ball ball)
    {
        this.ball = ball;
    }
    public void OnActivate()
    {
        ball.ballColor = (GameColors)Random.Range(0, System.Enum.GetValues(typeof(GameColors)).Length);
        ball.CurrentState = ball.inHTubeState;

    }

    public void OnDeactivate()
    {

    }

    public void OnUpdate()
    {

    }


}
