using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBallController
{

    /// <summary>
    /// Switch two balls
    /// @arg ball1: first ball
    /// @arg ball2: second ball
    /// @return true if balls are switched
    /// </summary>
    public bool Switch(Ball ball1, Ball ball2, Game game)
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

        Managers.Game.StartCoroutine(Managers.Game.DelayExecuter(game.CheckBallDrops, 0.3f));
        return true;
    }
}
