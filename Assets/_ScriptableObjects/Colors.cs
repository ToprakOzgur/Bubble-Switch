using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Colors", menuName = "ScriptableObjects/Colors", order = 1)]
public class Colors : ScriptableObject
{
    public Color[] normalBallColors;
    public Color blockBallColor;
}
