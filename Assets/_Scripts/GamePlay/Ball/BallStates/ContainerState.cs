using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerState : IBallState
{
    public static event Action<Ball> OnBallInContainer = delegate { };
    private readonly Ball ball;
    public ContainerState(Ball ball)
    {
        this.ball = ball;
    }
    public void OnActivate()
    {
        OnBallInContainer(ball);
    }

    public void OnDeactivate()
    {

    }

    public void OnUpdate()
    {

    }


}
