using System;
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
    private float spawnSpecialEffectCoefficient = 1f;

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

    private void OnEnable()
    {
        StartState.OnGameCreated += StartSpawn;
    }
    private void OnDisable()
    {
        StartState.OnGameCreated -= StartSpawn;
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
    public void StartSpawn(Game game)
    {
        StartCoroutine(SpawnLoop(game));
    }
    IEnumerator SpawnLoop(Game game)
    {
        while (isSpawning)
        {
            GetBall().Spawn();
            Debug.Log(game.SpawnRate * spawnSpecialEffectCoefficient);
            yield return new WaitForSeconds(game.SpawnRate * spawnSpecialEffectCoefficient);
        }
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
