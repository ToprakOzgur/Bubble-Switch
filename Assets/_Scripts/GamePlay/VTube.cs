using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VTube : MonoBehaviour
{
    public readonly int MaxBallCount = 10;
    [SerializeField]
    private GameColors tubeColor;

    [SerializeField]
    private Transform startPoint;
    [SerializeField]
    private Transform endPoint;

    private List<Ball> balls = new List<Ball>();

    public bool IsFull { get { return balls.Count == MaxBallCount; } }



    public void AddBall(Ball ball)
    {
        balls.Add(ball);
        ball.transform.SetParent(transform);
        var YOffSet = Vector3.up * 0.25f;
        ball.transform.position = startPoint.position + YOffSet + Vector3.up * (balls.Count - 1) * ((endPoint.position.y - startPoint.position.y) / MaxBallCount);
        ball.CurrentState = ball.inVTubeState;
    }
}

