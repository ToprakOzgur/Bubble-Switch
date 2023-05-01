using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBall : Ball
{

    public override void SetColor(GameColors ballColor)
    {
        this.currentBallColor = GameColors.None;
        GetComponent<SpriteRenderer>().color = colors.blockBallColor;
    }
}
