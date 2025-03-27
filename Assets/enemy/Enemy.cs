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
    [SerializeField] public Animator Animator;
    private float currChaseDistance;
    [SerializeField] public AudioSource audioSource;
    private bool audioPaused;

    public void SwitchState(BaseState state)
    {
        currentState.ExitState(this);
        currentState = state;
        currentState.EnterState(this);
    }

    public void Dead()
    {
        Destroy(gameObject);
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

        if(Time.timeScale == 0)
        {
            audioSource.Pause();
            audioPaused = true;
        }
        else if (Time.timeScale == 1 && audioPaused)
        {
            audioSource.UnPause();
            audioPaused = false;
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

    private void OnCollisionEnter(Collision collision)
    {
        if(currentState != RetreatState)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                if(collision.gameObject.GetComponent<Player>().isRespawn != true)
                {
                    collision.gameObject.GetComponent<Player>().Dead();
                }
            }
        }
    }
}   
