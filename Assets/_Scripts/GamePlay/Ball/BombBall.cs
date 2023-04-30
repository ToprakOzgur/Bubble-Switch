using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BombBall : Ball
{
    [SerializeField]
    protected TextMeshProUGUI text;
    [SerializeField]
    protected GameObject effect;
    [SerializeField]
    protected Vector3 effectSize;
    [SerializeField]
    protected int tickingTime;



    public override void SetColor(GameColors ballColor)
    {
        this.currentBallColor = GameColors.None;
    }
    public override void ActivateSpecialBallEffectInVTube()
    {
        text.gameObject.SetActive(true);
        StartCoroutine(StartClicking());
    }
    protected IEnumerator StartClicking()
    {
        // tickingTime = Random.Range(3, 20);
        //Counter
        while (tickingTime > 0)
        {
            text.text = tickingTime.ToString();
            yield return new WaitForSeconds(1);
            tickingTime--;

        }
        text.gameObject.SetActive(false);

        //Laser effect
        effect.SetActive(true);
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / 0.2f;

            effect.transform.localScale = Vector3.Lerp(new Vector3(0, 0.1f, 1), effectSize, t);
            yield return null;
        }
        t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / 0.2f;

            effect.transform.localScale = Vector3.Lerp(effectSize, new Vector3(0, 0.1f, 1), t);
            yield return null;
        }
        effect.SetActive(false);
        BombEffect();
    }

    protected virtual void BombEffect()
    {
        Managers.Game.currentGame.DeleteAreaAt(currentTube.index, GetBallIndexInVtube);
    }
}
