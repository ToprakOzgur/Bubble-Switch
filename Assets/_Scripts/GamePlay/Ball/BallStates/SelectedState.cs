using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedState : IBallState
{

    private readonly Ball ball;
    public SelectedState(Ball ball)
    {
        this.ball = ball;
    }
    public void OnActivate()
    {
        ball.selectionCircle.SetActive(true);
        Managers.Game.currentGame.AddBall(ball);
    }

    public void OnDeactivate()
    {
        ball.selectionCircle.SetActive(false);
        Managers.Game.currentGame.RemoveBall(ball);

    }

    public void OnMouseDown()
    {
        ball.CurrentState = ball.selectableState;
    }

    public void OnUpdate()
    {

    }
}
