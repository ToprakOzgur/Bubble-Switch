using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private int ballDropScore = 1;

    [SerializeField]
    private int multipleDropMultiplier = 5;
    private int currentScore = 0;

    private void OnEnable()
    {
        DropState.OnBallDropped += BallDropped;
        Game.OnMultipleDrop += MultipleDrop;
    }
    private void OnDisable()
    {
        DropState.OnBallDropped -= BallDropped;
        Game.OnMultipleDrop -= MultipleDrop;
    }



    private void BallDropped()
    {

        AddScore(ballDropScore);
    }
    private void MultipleDrop(int count)
    {
        AddScore(ballDropScore * count * multipleDropMultiplier);
    }
    public void AddScore(int amount)
    {
        currentScore += amount;
        Managers.UI.SetScore(currentScore);

    }
    public void ResetScore()
    {
        currentScore = 0;
        Managers.UI.SetScore(0);
    }
}
