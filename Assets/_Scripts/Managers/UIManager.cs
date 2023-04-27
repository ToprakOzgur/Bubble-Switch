using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    public void SetScore(int score)
    {
        scoreText.text = "SCORE : " + score.ToString();
    }
}
