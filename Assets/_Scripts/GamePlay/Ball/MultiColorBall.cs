using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiColorBall : Ball
{
    public override void SetColor(GameColors ballColor)
    {
        this.currentBallColor = GameColors.Multi;

    }
}
