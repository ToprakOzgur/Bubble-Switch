using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Ball ballPrefab;
    public Ball[] SpecialBallPrefab;
    public int poolSize = 50;
    [SerializeField]
    private Transform spawnPoint;

    private Queue<Ball> objectPool = new Queue<Ball>();

    private bool isSpawning = true;

    private void Awake()
    {
        for (int i = 0; i < poolSize; i++)
        {
            var ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
            ball.transform.SetParent(spawnPoint);
            ball.gameObject.SetActive(false);
            objectPool.Enqueue(ball);
        }
    }

    public Ball GetBall()
    {
        if (objectPool.Count == 0)
        {
            var ball = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);
            ball.transform.SetParent(spawnPoint);
            ball.gameObject.SetActive(false);
            objectPool.Enqueue(ball);
        }

        var pooledBall = objectPool.Dequeue();
        pooledBall.gameObject.SetActive(true);

        return pooledBall;
    }

    public void ReturnBall(Ball ball)
    {
        ball.gameObject.SetActive(false);
        objectPool.Enqueue(ball);
    }
    public void StartSpawn()
    {
        StartCoroutine(SpawnLoop());
    }
    IEnumerator SpawnLoop()
    {
        while (isSpawning)
        {
            GetBall().Spawn();
            yield return new WaitForSeconds(5);
        }

    }
}
