using UnityEngine;

public class ChaseState : MonoBehaviour, IEnemyState
{

    // TODO : tee neljäs tila joka on TRACKING STATE
    // tracking on sellainen että jos pelaaja menee nurkan taakse niin critter ei näe sitä 
    // tällöin critter menee viimeksi nähtyyn paikkaaan ja alerttaa siellä ja jos pelaaja ei ole siellä niin critter menee takaisin patrol tilaan
    // myös escape state joka lähtee pelaajaa vaan karkuun jhk vastaiseen suuntaan
    private StatePatternEnemy enemy;

    public ChaseState(StatePatternEnemy statePatternEnemy)
    {
        this.enemy = statePatternEnemy;
    }


    public void UpdateState()
    {
        enemy.indicator.material.color = Color.crimson;
        enemy.navMeshAgent.destination = enemy.target.position;
        enemy.navMeshAgent.isStopped = false;

        Look();
    }

    public void StateTriggerEnter(Collider other)
    {
        //if(other.CompareTag("Player"))
        //{
        //    print("critter " + enemy.name + " hit player in chase state");
        //}
    }

    public void StateTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("critter " + enemy.name + " hit player in chase state");
        }
    }

    void Look()
    {

        Debug.DrawRay(enemy.evil_eye.position, enemy.evil_eye.forward * enemy.sightRange, color: Color.green);

        if (Physics.Raycast(enemy.evil_eye.position, enemy.evil_eye.forward, out RaycastHit hit, enemy.sightRange) && hit.collider.CompareTag("Player"))
        {
            enemy.target = hit.collider.transform;
            ToChaseState();
        }

    }

    public void ToPatrolState()
    {
        print("Switched from chase to patrol state");
        enemy.currentState = enemy.patrolState;
    }
    public void ToAlertState()
    {
        print("Switched from chase to alert state");
        enemy.currentState = enemy.alertState;
    }
    public void ToChaseState()
    {
        print($"critter {enemy.name} tried going from {this.name} to {this.name} state");
    }
}
