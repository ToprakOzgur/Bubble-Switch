
using UnityEngine;

public class ArcadeGame : GameBase
{
    public ArcadeGame(VTube[] vTubes) : base(vTubes)
    {

    }

    public override bool didLost()
    {
        throw new System.NotImplementedException();
    }

    public override bool didWin()
    {
        throw new System.NotImplementedException();
    }
}
