using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ease
{
    // quote from: https://gist.github.com/xanathar/735e17ac129a72a277ee

    public static float BounceEaseOut(float t, float b, float c, float d)
    {
        if ((t /= d) < (1f / 2.75f))
            return c * (7.5625f * t * t) + b;
        else if (t < (2f / 2.75f))
            return c * (7.5625f * (t -= (1.5f / 2.75f)) * t + .75f) + b;
        else if (t < (2.5f / 2.75f))
            return c * (7.5625f * (t -= (2.25f / 2.75f)) * t + .9375f) + b;
        else
            return c * (7.5625f * (t -= (2.625f / 2.75f)) * t + .984375f) + b;
    }
}