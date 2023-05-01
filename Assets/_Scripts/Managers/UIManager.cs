using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{

    [SerializeField]
    protected TextMeshProUGUI scoreText;
    private void Start()
    {

        scoreText.text = "SCORE : " + 0;
    }
    public void SetScore(int score)
    {
        scoreText.text = "SCORE : " + score.ToString();
    }
}
