using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : BaseState
{
    public void EnterState(Enemy enemy)
    {
        Debug.Log("Start Chase");
    }
    public void UpdateState(Enemy enemy)
    {
        if(enemy.Player != null)
        {
            enemy.NavMeshAgent.destination = enemy.Player.transform.position;

            if(Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) > enemy.ChaseDistance)
            {
                enemy.SwitchState(enemy.PatrolState);
            }
        }
    }
    public void ExitState(Enemy enemy)
    {
        Debug.Log("Exit Chase");
    }
}
