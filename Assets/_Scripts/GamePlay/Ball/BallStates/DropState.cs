using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropState : IBallState
{
    public static event Action OnBallDropped = delegate { };

    private readonly Ball ball;
    private readonly float dropSpeed = 1f;
    public DropState(Ball ball)
    {
        this.ball = ball;

    }
    public void OnActivate()
    {
        ball.StartCoroutine(ball.MoveAnimation(ball.transform.position, Managers.WayPoints.dropPoint.position, dropSpeed));
        //reset ball and return to pool 
        OnBallDropped();
        ball.ActivateSpecialBallEffectInContainer();
    }

    public void OnDeactivate()
    {
    }

    public void OnMouseDown()
    {
        throw new System.NotImplementedException();
    }

    public void OnUpdate()
    {
    }
}
