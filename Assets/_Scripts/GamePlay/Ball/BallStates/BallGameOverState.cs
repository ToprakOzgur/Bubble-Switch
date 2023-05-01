using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallGameOverState : IBallState
{
    private readonly Ball ball;
    public BallGameOverState(Ball ball)
    {
        this.ball = ball;
    }
    public void OnActivate()
    {
        //some animation here
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
