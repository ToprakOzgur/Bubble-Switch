using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowBall : Ball
{
    private readonly int slowBallEffectDuration = 5;
    private readonly int spawnSlowChangeRation = 2;

    public override void ActivateSpecialBallEffectInContainer()
    {
        Managers.Game.currentGame.SlowDownGame(slowBallEffectDuration, spawnSlowChangeRation);
    }
}
