using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public Color[] colors;

    [HideInInspector]
    public GameColors ballColor;

    [HideInInspector]
    public VTube currentTube;
    public GameObject selectionCircle;

    [HideInInspector] public IBallState containerState;
    [HideInInspector] public IBallState inHTubeState;
    [HideInInspector] public IBallState inVTubeState;
    [HideInInspector] public IBallState switchTubeState;
    [HideInInspector] public IBallState spawnState;
    [HideInInspector] public IBallState dropState;
    [HideInInspector] public IBallState selectedState;
    [HideInInspector] public IBallState selectableState;



    private IBallState currentState;
    public IBallState CurrentState
    {
        get { return currentState; }
        set
        {
            currentState?.OnDeactivate();
            currentState = value;
            currentState?.OnActivate();
        }
    }

    public int GetBallIndexInVtube => currentTube.balls.IndexOf(this);
    private void Awake()
    {
        containerState = new ContainerState(this);
        inHTubeState = new InHTubeState(this);
        inVTubeState = new InVTubeState(this);
        switchTubeState = new SwitchTubesState(this);
        spawnState = new SpawnState(this);
        dropState = new DropState(this);
        selectedState = new SelectedState(this);
        selectableState = new SelectableState(this);
    }
    public void Spawn()
    {
        CurrentState = spawnState;
    }
    private void Update()
    {
        CurrentState.OnUpdate();
    }
    void OnMouseDown()
    {
        CurrentState.OnMouseDown();
    }
    public void SetColor(GameColors ballColor)
    {
        this.ballColor = ballColor;
        GetComponent<SpriteRenderer>().color = colors[(int)ballColor];
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
        Managers.Game.SetState(typeof(GamePlayState));
    }



}


