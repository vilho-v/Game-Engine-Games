using UnityEngine;

public class Targeting : MonoBehaviour
{
    public TurretEnemy[] turrets;
    public bool evil;

    private void Awake()
    {
        if(evil)
            GetComponent<MeshRenderer>().material.color = Color.red;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var turret in turrets)
            {
                turret.Shoot(other.transform);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!evil) return;
        if (other.CompareTag("Player"))
        {
            foreach (var turret in turrets)
            {
                turret.Shoot(other.transform);
            }
        }
    }
}

