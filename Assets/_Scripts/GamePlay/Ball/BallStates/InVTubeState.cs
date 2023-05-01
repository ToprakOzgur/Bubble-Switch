using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InVTubeState : IBallState
{

    private readonly Ball ball;
    private const float BallBounceTimeCoeff = 0.1f;


    public InVTubeState(Ball ball)
    {
        this.ball = ball;
    }
    public void OnActivate()
    {

        ball.transform.SetParent(ball.currentTube.transform);

        Vector3 startPosition = ball.currentTube.endPoint.transform.position;
        var YOffSet = Vector3.up * 0.25f;
        var targetPosition = ball.currentTube.startPoint.position + YOffSet + Vector3.up * (ball.currentTube.balls.Count - 1) * ((ball.currentTube.endPoint.position.y - ball.currentTube.startPoint.position.y) / ball.currentTube.MaxBallCount);
        var duration = BallBounceTimeCoeff * (ball.currentTube.MaxBallCount - ball.currentTube.balls.Count);

        ball.StartCoroutine(ball.BounceAnimation(startPosition, targetPosition, duration));

        ball.ActivateSpecialBallEffectInVTube();
    }

    public void OnDeactivate()
    {

    }

    public void OnUpdate()
    {

    }

    public void OnMouseDown()
    {

    }
}
