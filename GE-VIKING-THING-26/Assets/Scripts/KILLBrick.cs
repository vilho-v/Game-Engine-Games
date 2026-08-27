using JetBrains.Annotations;
using UnityEngine;

public class KILLBrick : MonoBehaviour
{

    public int damage = 15;

    // player is moved to checkpoint if tru
    // used for falling out of map 
    public bool banishPlayer = false;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);

                if (banishPlayer)
                {
                    player.transform.position = player.checkPoint;
                }
            }
        }
    }
}
