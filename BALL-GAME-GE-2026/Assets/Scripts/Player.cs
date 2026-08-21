using UnityEngine;



// this is the physical player BALL 
// it contains hp and ummmmmm 
public class Player : MonoBehaviour
{
    // plr can take dmg from blocks and stuff in the field
    public int health = 100, maxHealth = 100;


    // force to be applied to player
    public float force;

    Rigidbody rb;



    void Awake()
    {
        rb = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage(int damage)
    {
        health -= damage;

        // KILL
        if (health <= 0)
        {
            health = 0;
            Death();
        }

        // ui update ,,
        UIManager.Instance.UpdateHealth(health);
    }

    public void Death()
    {

    }

    void ApplyForce(Vector3 dir)
    {
        
    }
}
