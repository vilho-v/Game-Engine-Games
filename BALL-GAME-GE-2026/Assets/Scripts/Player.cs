using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// this is the physical player BALL 
// it contains hp and ummmmmm 
public class Player : MonoBehaviour
{
    // plr can take dmg from blocks and stuff in the field
    public int health = 100, maxHealth = 100;



    // ---- movement and physics things
    public bool flying = false;
    // force to be applied to player
    public float force;

    Rigidbody rb;

    public List<Collider> hitKillColliders = new();

    // when velocity is less than this on all axis, ball is stopped
    [SerializeField] Vector3 cutoff = new(0.1f, 0.1f, 0.1f);

    // WORLD pos
    [SerializeField] Vector3 mousePos;
    [SerializeField] Vector3 mouseScreen;
    [SerializeField] Vector3 dir;

    // line renderr 
    [SerializeField] LineRenderer line;





    void Awake()
    {
        rb = GetComponentInChildren<Rigidbody>();
    }



    // Update is called once per frame
    void Update()
    {
        // --- Mouse 
        // get world pos

        mouseScreen = Input.mousePosition;
        mouseScreen.z = Camera.main.transform.position.z * -1;
        mousePos = Camera.main.ScreenToWorldPoint(mouseScreen);

        // needs to be local to player otherwise it just stays in like one world spot and ruins everything
        mousePos = transform.InverseTransformPoint(mousePos);

        // adjust for 2d
        mousePos.z = 0;
        print(mousePos);


        // --- Input
        if(!flying)
        {
            dir = (transform.position + (mousePos * -1));
            if (Input.GetMouseButton(0))
            {
                print("m1 down");
                line.SetPosition(0, transform.position);
                line.SetPosition(1, dir);
                if (!line.enabled)
                    line.enabled = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                print("m1 up");
                ApplyForce(dir);
                line.enabled = false;
            }
        }




        // --- RB vel reset
        // set vel to ZERO when in motion and close to stopping
        if(rb.linearVelocity.x < cutoff.x && rb.linearVelocity.y < cutoff.y && flying)
        {
            flying = false;
            rb.linearVelocity = Vector3.zero;
        }
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
        UIManager.Instance.UpdateHealth($"{health}/{maxHealth}");
    }

    public void Death()
    {

    }



    void ApplyForce(Vector3 dir)
    {
        if (flying) return;
        flying = true;

        rb.AddForce(dir*force, ForceMode.Impulse);
    }
}
