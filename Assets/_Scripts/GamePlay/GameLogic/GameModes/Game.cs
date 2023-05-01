using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


//Game class is responsible for the game logic.This is  Arcade game.the other game modes will inherit from this class
public class Game
{
    public static Action OnGameLost = delegate { };
    public static Action<int> OnMultipleDrop = delegate { };
    public static event Action OnBallBombed = delegate { };
    protected VTube[] vTubes;

    #region  Logic Controllers
    protected SwitchBallController switchBallController = new SwitchBallController();
    protected BallDropController ballDropController = new BallDropController();
    #endregion
    protected List<Ball> selectedBalls = new List<Ball>();

    public Game(VTube[] vTubes)
    {
        this.vTubes = vTubes;
        ContainerState.OnBallInContainer += MoveToRandomTube;
    }

    ~Game()
    {
        ContainerState.OnBallInContainer -= MoveToRandomTube;
    }

    public virtual float SpawnRate
    {
        get
        {
            //difficulty is a value between 0 to 1.  increases over time.
            var difficulty = Mathf.Clamp01(Time.timeSinceLevelLoad / Managers.Game.gameSettings.secondsToMaxSpeed);

            //returns a value between startSpawnRate and minSpawnRate based on difficulty
            return Mathf.Lerp(Managers.Game.gameSettings.startSpawnRate, Managers.Game.gameSettings.minSpawnRate, difficulty);
        }
    }
    protected int GetTotalBallCountInVTubes()
    {
        return vTubes.Sum(vTube => vTube.balls.Count);
    }

    protected void MoveToRandomTube(Ball ball)
    {
        var emptyVTubes = Array.FindAll(vTubes, vTube => !vTube.IsFull);

        if (emptyVTubes.Length == 0)
        {
            MakeBallsDelesectable();
            OnGameLost();
            return;
        }

        var randomIndex = UnityEngine.Random.Range(0, emptyVTubes.Length);
        emptyVTubes[randomIndex].AddBall(ball);

    }

    private void MakeBallsDelesectable()
    {
        foreach (var vtube in vTubes)
        {
            foreach (var ball in vtube.balls)
            {
                ball.CurrentState = ball.gameOverState;
            }
        }
    }

    public void CheckBallDrops()
    {
        var dropBalls = ballDropController.CheckBallDrops(vTubes);

        if (dropBalls > 1)
            OnMultipleDrop(dropBalls);
    }
    protected bool Switch(Ball ball1, Ball ball2)
    {
        //balls are not in same row
        return switchBallController.Switch(ball1, ball2, this);
    }
    public void AddBall(Ball ball)
    {
        selectedBalls.Add(ball);

        if (selectedBalls.Count == 2)
        {
            if (!Switch(selectedBalls[0], selectedBalls[1]))
                Managers.Game.StartCoroutine(Managers.Game.DelayExecuter(ResetSelectedBalls, 0.2f));

        }
    }
    public void RemoveBall(Ball ball)
    {
        selectedBalls.Remove(ball);
    }
    public void ResetSelectedBalls()
    {
        selectedBalls[0].CurrentState = selectedBalls[0].selectableState;
        selectedBalls[0].CurrentState = selectedBalls[0].selectableState;
    }

    public void DeleteHorizontalLineAt(int getBallIndexInVtube)
    {
        foreach (var vTube in vTubes)
        {
            if (vTube.balls.Count > getBallIndexInVtube)
                ResetExplodedBallFromVtube(vTube, getBallIndexInVtube);
        }
    }

    public void DeleteAreaAt(int bombVtubeIndex, int getBallIndexInVtube)
    {
        // delete +1 and -1 index vertically and horizontally  adjacent  balls in Vtubes 

        foreach (var vTube in vTubes)
        {
            if (Math.Abs(vTube.index - bombVtubeIndex) <= 1)
            {
                if (vTube.balls.Count > getBallIndexInVtube && vTube.balls[getBallIndexInVtube] != null)
                    ResetExplodedBallFromVtube(vTube.balls[getBallIndexInVtube], vTube);

                if (vTube.balls.Count > getBallIndexInVtube && getBallIndexInVtube > 0 && vTube.balls[getBallIndexInVtube - 1] != null)
                    ResetExplodedBallFromVtube(vTube.balls[getBallIndexInVtube - 1], vTube);

                if (vTube.balls.Count > getBallIndexInVtube && getBallIndexInVtube < vTube.balls.Count - 1 && vTube.balls[getBallIndexInVtube + 1] != null)
                    ResetExplodedBallFromVtube(vTube.balls[getBallIndexInVtube + 1], vTube);

            }
        }
        foreach (var vTube in vTubes)
        {
            Managers.Game.StartCoroutine(Managers.Game.DelayExecuter(vTube.MoveDown, 0.3f));
        }
    }

    public void ResetExplodedBallFromVtube(Ball ball, VTube vtube)
    {
        ball.currentTube = null;
        ball.gameObject.SetActive(false);
        vtube.balls.Remove(ball);

        OnBallBombed();
    }
    public void ResetExplodedBallFromVtube(VTube vTube, int ballindex)
    {
        vTube.balls[ballindex].currentTube = null;
        vTube.balls[ballindex].gameObject.SetActive(false);
        vTube.balls.RemoveAt(ballindex);
        vTube.MoveDown();
        OnBallBombed();
    }
    public void SlowDownGame(int slowBallEffectDuration, int spawnSlowChangeRation)
    {
        //increase spawn ratio for slowBallEffectDuration seconds in spawn manager
        Managers.Spawner.DecreaseSpawnRatio(slowBallEffectDuration, spawnSlowChangeRation);

    }
    public void Freeze(int slowBallEffectDuration)
    {
        Managers.Spawner.Freeze(slowBallEffectDuration);
    }
}
