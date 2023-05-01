using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDropController
{
    /// <summary>
    /// Check if there is any ball to drop
    /// @arg vTubes: array of tubes
    /// @return number of balls dropped
    /// </summary>
    public int CheckBallDrops(VTube[] vTubes)
    {

        List<Ball> dropBalls = new List<Ball>();
        foreach (var vTube in vTubes)
        {
            List<Ball> currentTubeDrops = new List<Ball>();
            if (vTube.balls.Count > 0)
            {
                for (int i = 0; i < vTube.balls.Count; i++)
                {
                    if ((vTube.balls[i] is not BlockBall && vTube.balls[i].currentBallColor == vTube.tubeColor) || vTube.balls[i] is MultiColorBall)
                    {
                        dropBalls.Add(vTube.balls[i]);
                        currentTubeDrops.Add(vTube.balls[i]);
                    }
                    else
                    {
                        break;
                    }
                }

                foreach (var ball in dropBalls)
                {
                    ball.CurrentState = ball.dropState;
                    ball.currentTube = null;
                    vTube.balls.Remove(ball);
                }
                if (currentTubeDrops.Count > 0)
                    vTube.MoveDown();
            }

        }
        return dropBalls.Count;
    }
}
