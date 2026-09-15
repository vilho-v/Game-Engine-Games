using UnityEngine;

public class PatrolState : MonoBehaviour, IEnemyState
{
    private StatePatternEnemy enemy;

    int nextWaypoint = 0;

    public PatrolState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateState()
    {
        Look();
        Patrol();
    }

    void Look()
    {

        Debug.DrawRay(enemy.evil_eye.position, enemy.evil_eye.forward*enemy.sightRange, color: Color.green);

        if(Physics.Raycast(enemy.evil_eye.position, enemy.evil_eye.forward, out RaycastHit hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            enemy.target = hit.collider.transform;
            ToChaseState();
        }
    }

    void Patrol()
    {
        enemy.indicator.material.color = Color.green;

        // Set the destination of the NavMeshAgent to the next waypoint
        enemy.navMeshAgent.destination = enemy.waypoints[nextWaypoint].position;
        enemy.navMeshAgent.isStopped = false;

        // when at destination, go to next waypoint
        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            print($"{(nextWaypoint + 1) % enemy.waypoints.Length}");
            nextWaypoint = (nextWaypoint + 1) % enemy.waypoints.Length;
        }
    }

    public void StateTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            enemy.target = other.transform;
            ToAlertState();
        }
    }

    public void ToPatrolState()
    {
        print($"critter {enemy.name} tried going from {this.name} to {this.name} state");
    }
    public void ToAlertState()
    {
        print("switched from patrol to alert");
        enemy.currentState = enemy.alertState;
    }
    public void ToChaseState()
    {
        print("switched from patrol to chase");
        enemy.currentState = enemy.chaseState;
    }
}
