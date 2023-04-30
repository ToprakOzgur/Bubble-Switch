using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "AdventureData", menuName = "ScriptableObjects/AdventureData", order = 3)]
public class AdventureData : ScriptableObject
{
    public ColorData colorData;
}

[Serializable]
public struct ColorData
{
    public string adventureName;
    public Tube[] tubes;

}

[Serializable]
public struct Tube
{
    public GameColors[] balls;
}