using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableState : IBallState
{
    private readonly Ball ball;
    public SelectableState(Ball ball)
    {
        this.ball = ball;
    }
    public void OnActivate()
    {

    }

    public void OnDeactivate()
    {

    }

    public void OnMouseDown()
    {
        ball.CurrentState = ball.selectedState;
    }

    public void OnUpdate()
    {

    }
}
