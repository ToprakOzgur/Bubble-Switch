using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoSwitchBall : Ball
{
    protected override void OnMouseDown()
    {
        //disables the click event and so prevents entering to   the  selectState with overriding the base method.
        //can play a sound here.like error, warning, not clickable etc.
    }
}
