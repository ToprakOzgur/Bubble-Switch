using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{

    [SerializeField]
    protected TextMeshProUGUI scoreText;

    [SerializeField]
    private GameObject gameModeSelectPanel;
    private void OnEnable()
    {
        Game.OnGameLost += () => scoreText.text = "GAME OVER";
    }
    private void OnDisable()
    {
        Game.OnGameLost -= () => scoreText.text = "GAME OVER";
    }
    private void Start()
    {

        scoreText.text = "SCORE : " + 0;
    }
    public void SetScore(int score)
    {
        scoreText.text = "SCORE : " + score.ToString();
    }

    public void StartArcadeGame()
    {
        gameModeSelectPanel.SetActive(false);
        Managers.Game.StartArcadeGame();
    }
    public void StartZenGame()
    {
        gameModeSelectPanel.SetActive(false);
        Managers.Game.StartZenGame();

    }
    public void StartAdventureGame()
    {
        gameModeSelectPanel.SetActive(false);
        Managers.Game.StartAdventureGame();

    }
}
