using UnityEngine;
using UnityEngine.AI;
public class EscapeState : MonoBehaviour, IEnemyState
{
    private StatePatternEnemy enemy;

    Vector3 targetDir, targetPosition;
    bool running = false;

    public EscapeState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
    }

    public void UpdateState()
    {
        enemy.indicator.material.color = Color.lightSkyBlue;
        // jos juoksee niin ei etsi uutta pakoreittiä
        if(running)
        {
            // päästy loppuun
            if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && !enemy.navMeshAgent.pathPending)
            {
                running = false;
            }
        }
        else
        {
            GetEscapeNode();
        }
        
    }

    void GetEscapeNode()
    {
        if(running)
        {
            ToAlertState();
            running = false;
        }


        running = true;
        // yksikkövektori poispäin pelaajasta
        targetDir = (enemy.transform.position - enemy.target.position).normalized;

        // menee 10 metrin päähän
        targetPosition = enemy.transform.position + targetDir * 10;

        // lähe juoksemaan
        enemy.navMeshAgent.destination = targetPosition;
        enemy.navMeshAgent.isStopped = false;

    }

    public void StateTriggerStay(Collider other)
    {

    }

    public void ToPatrolState()
    {
        // never
    }
    public void ToAlertState()
    {
        //print("switched from patrol to alert");
        enemy.currentState = enemy.alertState;
    }
    public void ToChaseState()
    {
       // print("switched from patrol to chase");
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
        // never
    }
}
