using System;
using System.Collections.Generic;
using UnityEngine;


public class ZenGame : Game
{

    private List<float> last5DroppedBallTime = new List<float>();

    public ZenGame(VTube[] vTubes) : base(vTubes)
    {
        DropState.OnBallDropped += BallDropped;
    }

    ~ZenGame()
    {
        DropState.OnBallDropped -= BallDropped;
    }

    private void BallDropped()
    {
        //prevent intensive speed increase when multiple balls dropped at the same time
        if (last5DroppedBallTime.Count > 0 && Time.timeSinceLevelLoad - last5DroppedBallTime[last5DroppedBallTime.Count - 1] < 1)
            return;

        if (last5DroppedBallTime.Count > 2)
            last5DroppedBallTime.RemoveAt(0);

        last5DroppedBallTime.Add(Time.timeSinceLevelLoad);
    }

    public override float SpawnRate
    {
        get
        {
            //starting with default spawn rate until 3 dropped balls
            if (last5DroppedBallTime.Count < 3)
                return Managers.Game.gameSettings.zenStartSpawnRate;

            //calculate the average time between the last 5 dropped balls

            float averageTimeBetweenDroppedBalls = 0;
            for (int i = 0; i < last5DroppedBallTime.Count - 1; i++)
            {
                averageTimeBetweenDroppedBalls += last5DroppedBallTime[i + 1] - last5DroppedBallTime[i];
            }

            //Calculate the time without any dropped balls
            var timeWithoutDroppedBalls = Time.timeSinceLevelLoad - last5DroppedBallTime[last5DroppedBallTime.Count - 1];

            //returning average time between dropped balls + time without dropped balls
            var rate = (averageTimeBetweenDroppedBalls + timeWithoutDroppedBalls) / 3;

            return Math.Clamp(rate, 1, Mathf.Lerp(5, 20, GetTotalBallCountInVTubes() / 40));
        }
    }

}
