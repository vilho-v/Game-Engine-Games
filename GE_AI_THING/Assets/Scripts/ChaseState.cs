using UnityEngine;

public class ChaseState : MonoBehaviour, IEnemyState
{

    // TODO : tee nelj�s tila joka on TRACKING STATE
    // tracking on sellainen ett� jos pelaaja menee nurkan taakse niin critter ei n�e sit� 
    // t�ll�in critter menee viimeksi n�htyyn paikkaaan ja alerttaa siell� ja jos pelaaja ei ole siell� niin critter menee takaisin patrol tilaan
    // my�s escape state joka l�htee pelaajaa vaan karkuun jhk vastaiseen suuntaan
    private StatePatternEnemy enemy;



    public ChaseState(StatePatternEnemy statePatternEnemy)
    {
        this.enemy = statePatternEnemy;
    }


    public void UpdateState()
    {
        enemy.navMeshAgent.isStopped = false;
        enemy.indicator.material.color = Color.crimson;
        

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
        // if (other.CompareTag("Player"))
        // {
        //     print("critter " + enemy.name + " hit player in chase state");
        // }
    }

    void Look()
    {

        Debug.DrawRay(enemy.evil_eye.position, enemy.evil_eye.forward * enemy.sightRange, color: Color.green);

        if (Physics.SphereCast(enemy.evil_eye.position, 3,  enemy.evil_eye.forward, out RaycastHit hit, enemy.sightRange, layerMask: enemy.blockingMask) && hit.collider.CompareTag("Player"))
        {
            print("i see plr");
            if(enemy.player.evil)
            {
                ToEscapeState();
            }
            else
            {
                enemy.navMeshAgent.SetDestination(enemy.target.position);
            }
        }
        else
        {
            print("cant see plr");

            ToTrackState();
            
        }

    }

    public void ToPatrolState()
    {

        enemy.currentState = enemy.patrolState;
    }
    public void ToAlertState()
    {

        enemy.currentState = enemy.alertState;
    }
    public void ToChaseState()
    {
        //print($"critter {enemy.name} tried going from {this.name} to {this.name} state");
    }

    public void ToTrackState()
    {

        enemy.currentState = enemy.trackState;
    }

    public void ToEscapeState()
    {

        enemy.currentState = enemy.escapeState;
    }


}
