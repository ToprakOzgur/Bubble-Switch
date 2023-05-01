using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public Ball[] ballPrefabs;
    public int poolSize;


    [SerializeField]
    private Transform spawnPoint;

    private Queue<Ball>[] objectPool = new Queue<Ball>[8];

    private bool isSpawning = true;
    private float spawnSpecialEffectCoefficient = 1f;

    private void Awake()
    {
        for (int i = 0; i < objectPool.Length; i++)
        {
            objectPool[i] = new Queue<Ball>();
        }

        for (int j = 0; j < ballPrefabs.Length; j++)
        {
            for (int i = 0; i < poolSize; i++)
            {
                var ball = Instantiate(ballPrefabs[j], spawnPoint.position, Quaternion.identity);
                ball.transform.SetParent(spawnPoint);
                ball.gameObject.SetActive(false);
                objectPool[j].Enqueue(ball);
            }
        }

        Managers.Game.gameSettings.GetBallSpawnPercent(Managers.Game.gameSettings.normalBallsSpawnRate);
    }

    private void OnEnable()
    {
        GameManager.OnGameCreated += StartSpawn;
        Game.OnGameLost += () => isSpawning = false;
    }
    private void OnDisable()
    {
        GameManager.OnGameCreated -= StartSpawn;
        Game.OnGameLost -= () => isSpawning = false;
    }

    public Ball GetBall(int index)
    {
        if (objectPool[index].Count == 0)
        {
            var ball = Instantiate(ballPrefabs[index], spawnPoint.position, Quaternion.identity);
            ball.transform.SetParent(spawnPoint);
            ball.gameObject.SetActive(false);
            objectPool[index].Enqueue(ball);
        }

        var pooledBall = objectPool[index].Dequeue();
        pooledBall.gameObject.SetActive(true);

        return pooledBall;
    }

    public void ReturnBall(Ball ball, int index)
    {
        ball.gameObject.SetActive(false);
        objectPool[index].Enqueue(ball);
    }
    public void StartSpawn(Game game)
    {
        StartCoroutine(SpawnLoop(game));
    }
    IEnumerator SpawnLoop(Game game)
    {
        while (isSpawning)
        {
            var ballType = RandomSpawn();
            GetBall(ballType).Spawn();
            yield return new WaitForSeconds(game.SpawnRate * spawnSpecialEffectCoefficient);
        }
    }
    private int RandomSpawn()
    {
        int rand = UnityEngine.Random.Range(0, 100);

        int rangeStart = 0;
        int result = 0;
        for (int i = 0; i < ballPrefabs.Length; i++)
        {
            // Calculate this object's chance range.
            int rangeEnd = rangeStart + Managers.Game.gameSettings.GetChancesToSpawn()[i];

            if (rand >= rangeStart && rand < rangeEnd)
            {
                // If random number inside the range,
                // create the corresponding prefab.
                result = i;
            }

            // Next range right after the current one.
            rangeStart = rangeEnd;
        }
        return result;
    }

    public void DecreaseSpawnRatio(float duration, int spawnSlowChangeRation)
    {

        IEnumerator DecreaseSpawnRatioCoroutine(float duration)
        {
            spawnSpecialEffectCoefficient = spawnSlowChangeRation;
            yield return new WaitForSeconds(duration);
            spawnSpecialEffectCoefficient = 1;
        }
        StartCoroutine(DecreaseSpawnRatioCoroutine(duration));
    }

    public void Freeze(int duration)
    {
        if (isSpawning == false)
            return;

        IEnumerator Freeze(float duration)
        {
            isSpawning = false;
            yield return new WaitForSeconds(duration);
            isSpawning = true;
            StartCoroutine(SpawnLoop(Managers.Game.currentGame));
        }

        StartCoroutine(Freeze(duration));
    }
}
