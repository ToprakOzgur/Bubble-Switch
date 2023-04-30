using System;
using System.Collections.Generic;
using UnityEngine;


//Game class is responsible for the game logic.This is  Arcade game.the other game modes will inherit from this class
public class Game
{
    public static Action OnGameLost = delegate { };
    private VTube[] vTubes;

    private List<Ball> selectedBalls = new List<Ball>();

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


    private void MoveToRandomTube(Ball ball)
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

    public virtual bool DidWin() { return false; }

    public virtual bool DidLost() { return false; }

    public void CheckBallDrops()
    {
        Managers.Game.SetState(typeof(NotSwitchableState));

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
        if (dropBalls.Count > 0)
            Managers.Game.SetState(typeof(GamePlayState));
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

    private bool Switch(Ball ball1, Ball ball2)
    {
        //balls are not in same row
        if (ball1.GetBallIndexInVtube != ball2.GetBallIndexInVtube)
            return false;

        //balls are in the same v-Tube.
        if (ball1.currentTube.index == ball2.currentTube.index)
            return false;

        //balls are in not in adjacent v-Tubes.
        if (Mathf.Abs(ball1.currentTube.index - ball2.currentTube.index) > 1)
            return false;


        int index = ball1.GetBallIndexInVtube;

        ball1.currentTube.balls.Remove(ball1);
        ball2.currentTube.balls.Remove(ball2);

        ball1.currentTube.balls.Insert(index, ball2);
        ball2.currentTube.balls.Insert(index, ball1);

        var tmpTube = ball1.currentTube;

        ball1.currentTube = ball2.currentTube;
        ball2.currentTube = tmpTube;

        ball1.CurrentState = ball1.selectableState;
        ball2.CurrentState = ball2.selectableState;

        var tmpPos = ball1.transform.position;

        ball1.StartCoroutine(ball1.MoveAnimation(ball1.transform.position, ball2.transform.position, 0.2f));
        ball2.StartCoroutine(ball2.MoveAnimation(ball2.transform.position, tmpPos, 0.2f));

        Managers.Game.StartCoroutine(Managers.Game.DelayExecuter(CheckBallDrops, 0.3f));
        return true;
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
            {

                ResetABallFromVtube(vTube, getBallIndexInVtube);
            }
        }
    }

    public void DeleteAreaAt(int bombVtubeIndex, int getBallIndexInVtube)
    {
        // delete +1 and -1 index vertically and horizontally  adjacent  balls in Vtubes 

        foreach (var vTube in vTubes)
        {
            if (Math.Abs(vTube.index - bombVtubeIndex) <= 1)
            {
                ResetABallFromVtube(vTube, getBallIndexInVtube);
                ResetABallFromVtube(vTube, getBallIndexInVtube + 1);
                ResetABallFromVtube(vTube, getBallIndexInVtube - 1);
            }
        }
    }

    public void ResetABallFromVtube(VTube vTube, int ballindex)
    {
        if (ballindex < 0 || ballindex >= vTube.balls.Count)
            return;

        vTube.balls[ballindex].currentTube = null;
        vTube.balls[ballindex].gameObject.SetActive(false);
        vTube.balls.RemoveAt(ballindex);
        vTube.MoveDown();
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
