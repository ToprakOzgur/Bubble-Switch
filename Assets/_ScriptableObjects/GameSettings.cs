using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 2)]
public class GameSettings : ScriptableObject
{

    [Header("Arcade Mode Game Settings")]
    public int startSpawnRate;
    public int minSpawnRate;

    [Tooltip("Time to Reach maximum spawn rate in seconds")]
    public int secondsToMaxSpeed;

    [Header("General Game Settings")]

    public int ballSpeedInHtube;
}


