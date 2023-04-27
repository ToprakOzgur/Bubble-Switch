

using UnityEngine;

public abstract class GameBase
{
    protected GameBase()
    {
        //reset
        //init
        //start spawner
        Managers.Spawner.StartSpawn();

    }
    public abstract bool didWin();

    public abstract bool didLost();

}
