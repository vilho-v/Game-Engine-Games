using UnityEngine;

public class TrackState : MonoBehaviour, IEnemyState
{
    private StatePatternEnemy enemy;
    public TrackState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }


    public void UpdateState()
    {
        
        enemy.indicator.material.color = Color.pink;


        //enemy.navMeshAgent.SetDestination(enemy.target.transform.position);
        enemy.navMeshAgent.isStopped = false;
        
        Look();

        if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
        {
            ToAlertState();
        }
    }

    void Look()
    {

        Debug.DrawRay(enemy.evil_eye.position, enemy.evil_eye.forward*enemy.sightRange, color: Color.green);

        if(Physics.Raycast(enemy.evil_eye.position, enemy.evil_eye.forward, out RaycastHit hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            if(enemy.player.evil)
            {
                ToEscapeState();
            }
            else
            {
                enemy.target = hit.collider.transform;
                ToChaseState();
            }
        }
    }

    public void StateTriggerStay(Collider other)
    {

    }

    public void ToPatrolState()
    {
        //never
    }
    public void ToAlertState()
    {
        //print("switched from track to alert");
        enemy.currentState = enemy.alertState;
    }
    public void ToChaseState()
    {
        //print("switched from track to chase");
        enemy.currentState = enemy.chaseState;
    }

    public void StateTriggerEnter(Collider other)
    {
        //throw new System.NotImplementedException();
    }

    public void ToTrackState()
    {
        // never
    }

    public void ToEscapeState()
    {
        //print("switched from track to escape");
        enemy.currentState = enemy.escapeState;
    }
}
