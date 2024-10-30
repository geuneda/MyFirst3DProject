using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public Animator animator;
    public Transform[] waypoints;
    private int currentWaypointIndex;
    private NavMeshAgent agent;
    private INPCState currentState;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator.speed = 1.0f;
        currentState = new IdleState();
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(INPCState newState)
    {
        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public void StartMoveToTarget()
    {
        if (waypoints.Length > 0)
        {
            agent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }

    public bool AtTarget()
    {
        return !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance;
    }

    public void SetNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }
}

