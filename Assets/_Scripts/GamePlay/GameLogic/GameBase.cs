using System;
using UnityEngine;

public abstract class GameBase
{
    public static Action OnGameLost = delegate { };
    private VTube[] vTubes;
    protected GameBase(VTube[] vTubes)
    {
        this.vTubes = vTubes;
        ContainerState.OnBallInContainer += BallInContainer;
        Managers.Spawner.StartSpawn();

    }
    ~GameBase()
    {
        ContainerState.OnBallInContainer -= BallInContainer;
    }

    private void BallInContainer(Ball ball)
    {

        var emptyVTubes = Array.FindAll(vTubes, vTube => !vTube.IsFull);

        if (emptyVTubes.Length == 0)
        {
            Debug.Log("Game Over");
            OnGameLost();
            return;
        }
        var randomIndex = UnityEngine.Random.Range(0, emptyVTubes.Length);
        emptyVTubes[randomIndex].AddBall(ball);

    }

    public abstract bool didWin();

    public abstract bool didLost();

}
