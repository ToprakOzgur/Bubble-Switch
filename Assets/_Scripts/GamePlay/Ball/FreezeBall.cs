using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezeBall : Ball
{
    private readonly int slowBallEffectDuration = 10;
    public override void ActivateSpecialBallEffectInContainer()
    {
        Managers.Game.currentGame.Freeze(slowBallEffectDuration);

    }
}
