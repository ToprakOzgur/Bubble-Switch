using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(GameManager))]
[RequireComponent(typeof(UIManager))]
[RequireComponent(typeof(ScoreManager))]
[RequireComponent(typeof(SpawnManager))]
[RequireComponent(typeof(WayPointsManager))]
public class Managers : MonoBehaviour
{
    public static GameManager Game { get; private set; }
    public static UIManager UI { get; private set; }
    public static ScoreManager Score { get; private set; }
    public static SpawnManager Spawner { get; private set; }
    public static WayPointsManager WayPoints { get; private set; }

    void Awake()
    {
        Game = GetComponent<GameManager>();
        UI = GetComponent<UIManager>();
        Score = GetComponent<ScoreManager>();
        Spawner = GetComponent<SpawnManager>();
        WayPoints = GetComponent<WayPointsManager>();
    }
}
