using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BaseState
{
    public void EnterState(Enemy enemy);
    public void UpdateState(Enemy enemy);
    public void ExitState(Enemy enemy);
}
