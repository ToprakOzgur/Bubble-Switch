using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameBase
{
    public static Action OnGameLost = delegate { };
    private VTube[] vTubes;

    private List<Ball> selectedBalls = new List<Ball>();
    protected GameBase(VTube[] vTubes)
    {
        this.vTubes = vTubes;
        ContainerState.OnBallInContainer += MoveToRandomTube;
        Managers.Spawner.StartSpawn();
    }
    ~GameBase()
    {
        ContainerState.OnBallInContainer -= MoveToRandomTube;
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

    public abstract bool didWin();

    public abstract bool didLost();

    public void CheckBallDrops()
    {
        Managers.Game.SetState(typeof(NotSwitchableState));

        foreach (var vTube in vTubes)
        {

            if (vTube.balls.Count > 0)
            {
                List<Ball> dropBalls = new List<Ball>();

                for (int i = 0; i < vTube.balls.Count; i++)
                {
                    if (vTube.balls[i].ballColor == vTube.tubeColor)
                    {
                        dropBalls.Add(vTube.balls[i]);
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

                if (dropBalls.Count > 0)
                {
                    return;
                }
            }

        }
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

        return true;
    }
    public void ResetSelectedBalls()
    {
        selectedBalls[0].CurrentState = selectedBalls[0].selectableState;
        selectedBalls[0].CurrentState = selectedBalls[0].selectableState;
    }
}
