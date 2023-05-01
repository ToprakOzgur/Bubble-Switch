using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VTube : MonoBehaviour
{
    public readonly int MaxBallCount = 10;

    public int index;
    [HideInInspector]
    public List<Ball> balls = new List<Ball>();

    public Transform startPoint;
    public Transform endPoint;
    public GameColors tubeColor;
    public bool IsFull { get { return balls.Count == MaxBallCount; } }

    public void AddBall(Ball ball)
    {
        balls.Add(ball);
        ball.currentTube = this;
        ball.CurrentState = ball.inVTubeState;
    }

    /// <summary>
    /// Move balls down if there is empty slot
    /// </summary>

    public void MoveDown()
    {


        List<Ball> ballsToMove = new List<Ball>();  // balls to move down
        foreach (var ball in balls)
        {
            if (ball != null)
            {
                ballsToMove.Add(ball);
            }
        }
        balls.Clear();

        foreach (var ball in ballsToMove)
        {
            balls.Add(ball);
            ball.currentTube = this;
            ball.MoveToEmptySlots();
        }
    }
}

