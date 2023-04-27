using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int currentScore = 0;

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
