using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LaserBall : BombBall
{
    protected override void BombEffect()
    {
        Managers.Game.currentGame.DeleteHorizontalLineAt(GetBallIndexInVtube);
    }
}
