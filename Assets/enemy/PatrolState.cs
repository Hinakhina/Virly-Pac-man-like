using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    private bool isMoving;
    private Vector3 destination;

    public void EnterState(Enemy enemy)
    {
        isMoving = false;
        if(enemy != null)
        {
            enemy.Animator.SetTrigger("PatrolState");
        }
        Debug.Log("Start Patrol");
    }
    public void UpdateState(Enemy enemy)
    {
        if(Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) < enemy.ChaseDistance && enemy.Player.isRespawn != true)
        {
            enemy.SwitchState(enemy.ChaseState);
        }
        if(!isMoving)
        {
            isMoving = true;
            int index = UnityEngine.Random.Range(0, enemy.Waypoints.Count);
            destination = enemy.Waypoints[index].position;
            enemy.NavMeshAgent.destination = destination;
        }
        else
        {
            float distance = Vector3.Distance(destination, enemy.transform.position);
            if(distance <= 0.17)
            {
                isMoving = false;
            }
        }
    }
    public void ExitState(Enemy enemy)
    {
        Debug.Log("Exit Patrol");
    }
}
