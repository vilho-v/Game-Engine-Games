using UnityEngine;

public class AlertState : MonoBehaviour, IEnemyState
{
    private StatePatternEnemy enemy;

    float searchTimer;

    public AlertState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
       
    }

    void Search()
    {
        enemy.navMeshAgent.isStopped = true;
        enemy.transform.Rotate(0, enemy.searchRotationSpeed * Time.deltaTime, 0);

        searchTimer += Time.deltaTime;

        if (searchTimer >= enemy.searchDuration)
        {
            searchTimer = 0;
            ToPatrolState();
        }
    }
    void Look()
    {

        Debug.DrawRay(enemy.evil_eye.position, enemy.evil_eye.forward * enemy.sightRange, color: Color.green);

        if (Physics.Raycast(enemy.evil_eye.position, enemy.evil_eye.forward, out RaycastHit hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
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

    public void UpdateState()
    {
         enemy.indicator.material.color = Color.yellow;
        Look();
        Search();
        
    }

    public void StateTriggerEnter(Collider other)
    {

    }
    public void StateTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           // print("critter " + enemy.name + " hit player in chase state");
        }
    }

    public void ToPatrolState()
    {
        searchTimer = 0;
        enemy.currentState = enemy.patrolState;
    }
    public void ToAlertState()
    {
        //print($"critter {enemy.name} tried going from {this.name} to {this.name} state");
    }
    public void ToChaseState()
    {
       // print("switched from alert to chase state");
        enemy.currentState = enemy.chaseState;
    }
    public void ToTrackState()
    {

    }

    public void ToEscapeState()
    {
        
    }

}
