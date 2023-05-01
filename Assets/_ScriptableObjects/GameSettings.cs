using System;
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


    [Header("Zen Mode Game Settings")]
    public int zenStartSpawnRate;

    [Header("General Game Settings")]

    public int ballSpeedInHtube;


    #region Ball Spawn Rates

    [Header("Ball Spawn Rates")]


    [Range(0, 100)]
    public int normalBallsSpawnRate = 50;


    [Range(0, 100)]
    public int freezeSpawnRate = 50;


    [Range(0, 100)]
    public int multiSpawnRate = 50;


    [Range(0, 100)]
    public int slowSpawnRate = 50;


    [Range(0, 100)]
    public int LaserSpawnRate = 50;


    [Range(0, 100)]
    public int bombSpawnRate = 50;


    [Range(0, 100)]
    public int noSwitchSpawnRate = 50;


    [Range(0, 100)]
    public int blockSpawnRate = 50;

    #endregion

    public int GetBallSpawnPercent(int ballRate)
    {

        var total = normalBallsSpawnRate + freezeSpawnRate + multiSpawnRate + slowSpawnRate + LaserSpawnRate + bombSpawnRate + noSwitchSpawnRate + blockSpawnRate;

        return (int)(((float)ballRate / total) * 100);
    }

    public int[] GetChancesToSpawn()
    {
        int[] chances = new int[8];
        chances[0] = GetBallSpawnPercent(normalBallsSpawnRate);
        chances[1] = GetBallSpawnPercent(freezeSpawnRate);
        chances[2] = GetBallSpawnPercent(multiSpawnRate);
        chances[3] = GetBallSpawnPercent(slowSpawnRate);
        chances[4] = GetBallSpawnPercent(LaserSpawnRate);
        chances[5] = GetBallSpawnPercent(bombSpawnRate);
        chances[6] = GetBallSpawnPercent(noSwitchSpawnRate);
        chances[7] = GetBallSpawnPercent(blockSpawnRate);
        return chances;
    }
}




