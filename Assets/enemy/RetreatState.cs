using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetreatState : BaseState
{
    public void EnterState(Enemy enemy)
    {
        if(enemy != null)
        {
            enemy.Animator.SetTrigger("RetreatState");
        }
        Debug.Log("Start Retreat");
    }
    public void UpdateState(Enemy enemy)
    {
        if(enemy.Player != null)
        {
            enemy.NavMeshAgent.destination = enemy.transform.position + (enemy.transform.position - enemy.Player.transform.position).normalized;
        }
    }
    public void ExitState(Enemy enemy)
    {
        Debug.Log("Exit Retreat");
    }
}
