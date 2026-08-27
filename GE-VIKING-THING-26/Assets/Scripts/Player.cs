using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public int health = 100, maxHealth = 100, maxMaxHealth_final_2_1;
    Animator anim;
    public float jumpForce=1, moveSpeed=5, maxSpeed=10;

    bool mirrored;

    Rigidbody rb;

    public bool grounded;

    public Vector3 checkPoint;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        checkPoint = transform.position;
    }


    float jumpTimer;
    // Update is called once per frame
    void Update()
    {


        if(Input.GetKeyDown("1"))
        {
            anim.SetTrigger("Axe");
        }


        if(Input.GetAxisRaw("Horizontal") != 0)
        {
  
            if (rb.linearVelocity.magnitude < maxSpeed)
                //rb.AddForce(Input.GetAxis("Horizontal") * Vector2.right * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
                //rb.AddForce(Input.GetAxis("Horizontal") * Vector2.right * moveSpeed, ForceMode.VelocityChange);
                rb.linearVelocity = Vector3.MoveTowards(rb.linearVelocity, new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.linearVelocity.y, 0), moveSpeed * Time.deltaTime);

            // move position RUINS inertia !!!
            //rb.MovePosition(transform.position + Input.GetAxisRaw("Horizontal") * Vector3.right * moveSpeed * Time.deltaTime);
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"),1,1);
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        //if (Input.GetKey(leftKey))
        //{
        //    if (!mirrored)
        //    {
        //        transform.localScale = new Vector3(-1, 1, 1);
        //        mirrored = true;
        //    }
        //    MoveTo(Vector3.left);
        //}
        //else if (Input.GetKey(rightKey))
        //{
        //    if (mirrored)
        //    {
        //        transform.localScale = new Vector3(1, 1, 1);
        //        mirrored = false;
        //    }
        //    MoveTo(Vector3.right);
        //}
        //else
        //{   
        //    anim.SetBool("Walk", false);
        //}
        //

        if(jumpTimer > 0)
        {
            jumpTimer -= Time.deltaTime;
        }

        else if (Input.GetButton("Jump"))
        {
            if (grounded)
            {
                grounded = false;
                jumpTimer = 0.5f;
                Jump();
            }
        }

    }

    private void OnTriggerStay(Collider col)
    {
        print($"Collided with {col.name}");
        if (col.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    //void MoveTo(Vector3 dir)
    //{
    //    anim.SetBool("Walk", true);
    //    rb.MovePosition(transform.position + dir * Time.deltaTime * moveSpeed);
    //}

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
        anim.SetTrigger("Jump");
    }

    public void JumpEndEvent()
    {
         
    }


    public void TakeDamage(int damage)
    {
        health -= damage;
        UIManager.instance.UpdateHealth(health);
        if (health <= 0)
        {
            Death();
        }
    }

    // update hp (called by pickups)
    public void Heal(int healing)
    {
        if(health + healing > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health += healing;
        }

        UIManager.instance.UpdateHealth(health);
    }

    // dying resets hp and makes plr go back to prev checkpoint
    void Death()
    {
        transform.position = checkPoint;    
        //anim.SetTrigger("Death");
        //rb.isKinematic = true;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        health = maxHealth;

        UIManager.instance.UpdateHealth(health, true);
    }

    public void CheckPointThing()
    {
        checkPoint = transform.position;
        Heal(1000);
    }
}
