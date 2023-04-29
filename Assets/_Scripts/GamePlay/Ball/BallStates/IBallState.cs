using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBallState
{
    void OnActivate();
    void OnDeactivate();
    void OnUpdate();
    void OnMouseDown();

}
