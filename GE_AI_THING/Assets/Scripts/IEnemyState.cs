using UnityEngine;

public interface IEnemyState
{


    public void UpdateState();

    public void StateTriggerEnter(Collider other);

    public void StateTriggerStay(Collider other);

    public void ToPatrolState();
    public void ToAlertState();
    public void ToChaseState();
}
