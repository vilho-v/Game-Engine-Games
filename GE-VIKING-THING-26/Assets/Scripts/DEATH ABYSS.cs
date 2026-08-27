using UnityEngine;

public class DEATHABYSS : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(player.maxHealth);
                player.transform.position = player.checkPoint;
            }
        }
    }
}

