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
        var ballColor = (GameColors)Random.Range(0, ball.colors.normalBallColors.Length);
        ball.SetColor(ballColor);

        ball.CurrentState = ball.inHTubeState;

    }

    public void OnDeactivate()
    {

    }

    public void OnMouseDown()
    {

    }

    public void OnUpdate()
    {

    }


}
