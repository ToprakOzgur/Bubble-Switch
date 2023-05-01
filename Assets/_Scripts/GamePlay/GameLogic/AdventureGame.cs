
using System.Linq;
using UnityEngine;

public class AdventureGame : Game
{
    public AdventureGame(VTube[] vTubes) : base(vTubes)
    {
        Initialize(vTubes);
    }
    private void Initialize(VTube[] vTubes)
    {
        var data = Managers.Game.adventureData;
        for (int i = 0; i < vTubes.Length; i++)
        {
            var v = vTubes.ToList().First(x => x.index == i + 1);

            for (int j = 0; j < data.colorData.tubes[i].balls.Length; j++)
            {
                //manually add balls to the tube
                var ball = Managers.Spawner.GetBall(0);
                ball.SetColor(data.colorData.tubes[i].balls[j]);
                v.balls.Add(ball);
                ball.currentTube = v;
                ball.CurrentState = ball.selectableState;
                ball.transform.SetParent(v.transform);

                //locate balls in the tube
                var YOffSet = Vector3.up * 0.25f;
                var targetPosition = ball.currentTube.startPoint.position + YOffSet + Vector3.up * (ball.currentTube.balls.Count - 1) * ((ball.currentTube.endPoint.position.y - ball.currentTube.startPoint.position.y) / ball.currentTube.MaxBallCount);
                ball.transform.position = targetPosition;
            }
        }
        CheckBallDrops();
    }
}
