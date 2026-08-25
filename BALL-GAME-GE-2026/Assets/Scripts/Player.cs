using UnityEngine;
using System.Collections;
using System.Collections.Generic;


// this is the physical player BALL 
// containts hp and movement Things
public class Player : MonoBehaviour
{
    // plr can take dmg from blocks and stuff in the field
    public int health = 100, maxHealth = 100;

    // idk temporary damage value for testing
    public float damage;
    public float highPoint;

    // ---- movement and physics things
    public bool flying = false, goingDown = false;

    // force mult to be applied to player by INPUT
    public float force;


    [HideInInspector] public Rigidbody rb;

    public List<KillGround> hitKillColliders = new();

    // when velocity is less than this on all axis, ball is stopped
    [SerializeField] Vector3 cutoff = new(0.1f, 0.1f, 0.1f);

    // WORLD pos
    [SerializeField] Vector3 mousePos;
    [SerializeField] Vector3 mouseScreen;
    [SerializeField] Vector3 dir;

    // cool arrow which points where the player is going to fly
    [SerializeField] LineRenderer line;

    // where the player will respawn if they die - todo checkpoint
    public Vector3 respawnPos;

    // timer so rb velocity isnt cucked right at launch 
    [SerializeField]float timer;

    // mask used by inactive platforms
    LayerMask platformMask;

    void Awake()
    {
        // it might be in children
        rb = GetComponentInChildren<Rigidbody>();

        // get init position
        respawnPos = transform.position;
        platformMask = LayerMask.GetMask("PlatformInactive");
    }



    // Update is called once per frame
    void Update()
    {

        // --- Mouse 
        // get world pos
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // adjust for 2d
        mousePos.z = 0;

        // get direction from player to mouse pos
        dir = transform.TransformPoint(transform.position - (mousePos));


        if (!goingDown && rb.linearVelocity.y < -0.01)
        {
            goingDown = true;
            highPoint = transform.position.y;   
        }
        // --- Input
        if(!flying)
        {

            // holding down mouse - draw line from player to mouse pos
            if (Input.GetMouseButton(0))
            {
                print("m1 down");
                line.SetPosition(0, transform.position);
                line.SetPosition(1, dir);
                if (!line.enabled)
                    line.enabled = true;
            }

            // mouse up
            if (Input.GetMouseButtonUp(0))
            {
                // cooldown for velocity reset
                timer = 0.5f;
                print("m1 up");

                // force has to be relative to player so it is applied in the correct direction
                ApplyForce(transform.InverseTransformPoint(dir));

                line.enabled = false;
                goingDown = false;
                flying = true;
            }
        }

        // enable platform underneath player if it is inactive
        GroundDetectThing();


        // Timer so player movement isnt halted immediately by rb velocity being too low
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }


        //---RB vel reset
        // set vel to ZERO when in motion and close to stopping
        //print(rb.linearVelocity);
        if (goingDown && rb.linearVelocity.magnitude < cutoff.magnitude)
        {
            ResetMovement();
        }
    }

    // constantly casts a ray and activates a platform if the player is directly over it
    void GroundDetectThing()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 10, platformMask))
        {
            if (!hit.collider.CompareTag("Platform"))
                return;

            if(hit.collider.TryGetComponent(out Platform platform))
            { 
                platform.TogglePlatform(true);
            }
        }
    }

    void ResetMovement()
    {
        flying = false;
        goingDown = false;
        rb.linearVelocity = Vector3.zero;

        foreach(KillGround kg in hitKillColliders)
        {
            if (kg != null)
            {
                kg.ToggleKillThing(true);
            }
        }
        hitKillColliders.Clear();
    }

    public void Damage(int damage)
    {
        this.damage = damage;
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

    // tp plr back to the bottom
    public void Death()
    {
        foreach(Platform pt in FindObjectsByType<Platform>(sortMode: FindObjectsSortMode.None))
        {
            pt.TogglePlatform(false);
        }

        ResetMovement();
        transform.position = respawnPos;
        health = maxHealth;
        damage = 0;

    }



    public void ApplyForce(Vector3 dir, bool forced = false)
    {
        if (flying && !forced) return;

        rb.AddForce(dir*force, ForceMode.Impulse);
        flying = true;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (goingDown)
            if (collision.gameObject.TryGetComponent(out Platform platform))
            {

                damage = Mathf.Sqrt(2f*Mathf.Abs(Physics.gravity.y) * Mathf.Abs(highPoint - transform.position.y));
                print($"hit platform {platform.name}");
                Damage((int)damage);
                goingDown = false;
            

            }   
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    if(collision.gameObject.CompareTag("Ground"))
    //    {
    //        flying = false;
    //        goingDown = false;
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if(collision.gameObject.CompareTag("Ground"))
    //    {
    //        flying = true;
           
    //    }
    //}
}

