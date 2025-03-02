using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] public List<Transform> Waypoints = new List<Transform>();
    private BaseState currentState;
    [HideInInspector] public PatrolState PatrolState = new PatrolState();
    [HideInInspector] public ChaseState ChaseState= new ChaseState();
    [HideInInspector] public RetreatState RetreatState= new RetreatState();
    [SerializeField] public NavMeshAgent NavMeshAgent;
    [SerializeField] public float ChaseDistance;
    [SerializeField] public Player Player;
    private float currChaseDistance;

    public void SwitchState(BaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }
    private void Awake()
    {
        currentState = PatrolState;
        currentState.EnterState(this);
        currChaseDistance = ChaseDistance;
    } 

    private void Start()
    {
        if(Player != null)
        {
            Player.OnPowerUpStart += StartRetreating;
            Player.OnPowerUpStop += StopRetreating;
        }
    }

    private void Update(){
        if(Player.SneakMode == true)
        {
            ChaseDistance = 2;
        }
        else{
            ChaseDistance = currChaseDistance;
        }

        if(currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    private void StartRetreating()
    {
        SwitchState(RetreatState);
    }

    private void StopRetreating()
    {
        SwitchState(PatrolState);
    }
}   
