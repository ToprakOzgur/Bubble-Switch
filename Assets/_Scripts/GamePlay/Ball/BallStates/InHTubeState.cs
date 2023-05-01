using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InHTubeState : IBallState
{
    private readonly Ball ball;

    private float minDistance = 0.04f;
    private int currentWaypointIndex = 0;
    public InHTubeState(Ball ball)
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
    }

    public void OnUpdate()
    {
        float distanceToWaypoint = (ball.transform.position - Managers.WayPoints.points[currentWaypointIndex].position).sqrMagnitude;

        if (distanceToWaypoint <= minDistance)
        {
            ball.transform.position = Managers.WayPoints.points[currentWaypointIndex].position;
            currentWaypointIndex = (currentWaypointIndex + 1);

            if (currentWaypointIndex == Managers.WayPoints.points.Length)
            {
                ball.CurrentState = ball.containerState;
                return;
            }
        }

        ball.transform.position = Vector3.MoveTowards(ball.transform.position, Managers.WayPoints.points[currentWaypointIndex].position, Managers.Game.gameSettings.ballSpeedInHtube * Time.deltaTime);
    }

}
