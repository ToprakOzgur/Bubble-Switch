using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public Colors colors;

    [HideInInspector]
    public GameColors currentBallColor;

    [HideInInspector]
    public VTube currentTube;
    public GameObject selectionCircle;

    [HideInInspector] public IBallState containerState;
    [HideInInspector] public IBallState inHTubeState;
    [HideInInspector] public IBallState inVTubeState;
    [HideInInspector] public IBallState spawnState;
    [HideInInspector] public IBallState dropState;
    [HideInInspector] public IBallState selectedState;
    [HideInInspector] public IBallState selectableState;

    [HideInInspector] public IBallState gameOverState;


    protected IBallState currentState;
    public IBallState CurrentState
    {
        get { return currentState; }
        set
        {
            if (currentState == value)
                return;
            currentState?.OnDeactivate();
            currentState = value;
            currentState?.OnActivate();
        }
    }

    public int GetBallIndexInVtube => currentTube.balls.IndexOf(this);
    protected void Awake()
    {
        containerState = new ContainerState(this);
        inHTubeState = new InHTubeState(this);
        inVTubeState = new InVTubeState(this);
        spawnState = new SpawnState(this);
        dropState = new DropState(this);
        selectedState = new SelectedState(this);
        selectableState = new SelectableState(this);
        gameOverState = new BallGameOverState(this);
    }
    public void Spawn()
    {
        CurrentState = spawnState;
    }
    protected void Update()
    {
        CurrentState.OnUpdate();
    }
    protected virtual void OnMouseDown()
    {
        CurrentState.OnMouseDown();
    }
    public virtual void SetColor(GameColors ballColor)
    {
        this.currentBallColor = ballColor;
        GetComponent<SpriteRenderer>().color = colors.normalBallColors[(int)ballColor];
    }
    public IEnumerator BounceAnimation(Vector3 startPosition, Vector3 targetPosition, float duration, bool isBouncing = true)
    {
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / duration;

            transform.position = Vector3.Lerp(startPosition, targetPosition, Ease.BounceEaseOut(t, 0f, 1f, 0.5f));
            yield return null;
        }
        CurrentState = selectableState;

        if (GetBallIndexInVtube == 0)
            Managers.Game.currentGame.CheckBallDrops();

    }
    public IEnumerator MoveAnimation(Vector3 startPosition, Vector3 targetPosition, float duration)
    {
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / duration;

            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

    }

    public virtual void ActivateSpecialBallEffectInVTube()
    {
        //overriding in child classes
    }
    public virtual void ActivateSpecialBallEffectInContainer()
    {
        //overriding in child classes
    }


    public void MoveToEmptySlots()
    {
        transform.SetParent(currentTube.transform);

        Vector3 startPosition = transform.position;
        var YOffSet = Vector3.up * 0.25f;
        var targetPosition = currentTube.startPoint.position + YOffSet + Vector3.up * (currentTube.balls.Count - 1) * ((currentTube.endPoint.position.y - currentTube.startPoint.position.y) / currentTube.MaxBallCount);
        var duration = 0.2f;

        StartCoroutine(MoveAnimation(startPosition, targetPosition, duration));
    }
}


