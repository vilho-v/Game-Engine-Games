using UnityEngine;

public class KillGround : MonoBehaviour
{

    // this has a SINGLE collider which is added to a player list of previously hit colliders
    Collider col;
    public int damage = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(!TryGetComponent(out col))
        {
            print($"KILL brick: forgot to put COLLIDER on {gameObject.name}");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        // get plr and if hasnt collided with this yet , deal dmg
        if(collision.gameObject.TryGetComponent(out Player player) && !player.hitKillColliders.Contains(col))
        {
            player.hitKillColliders.Add(col);
            player.Damage(damage);
        }
    }
}

