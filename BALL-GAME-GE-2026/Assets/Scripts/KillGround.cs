using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Renderer))]

public class KillGround : MonoBehaviour
{


    // this has a SINGLE collider which is added to a player list of previously hit colliders
    Collider col;
    public int damage = 10;
    public float knockback = 5;


    [Header("Set in editor")]
    public Color inactiveColor;

    Color activeColor;

    ParticleSystem ps;

    bool active = true;

    void Start()
    {
        if (!TryGetComponent(out col))
        {
            print($"KILL brick: forgot to put COLLIDER on {gameObject.name}");
        }
        TryGetComponent(out ps);
        activeColor = GetComponent<Renderer>().material.color;

        ToggleKillThing(true);
    }

    private void OnCollisionEnter(Collision collision)
    {

        // get plr and if hasnt collided with this yet , deal dmg
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            // only apply dmg once but apply kb always
            if (active)
            {

                player.hitKillColliders.Add(this);
                player.Damage(damage);
                ToggleKillThing(false);
            }

            player.ApplyForce((player.transform.position - transform.position).normalized * knockback, true);
        }
    }

    public void ToggleKillThing(bool toggle)
    {
        if (active == toggle) return;
        active = toggle;

        if (toggle)
        {
            UpdateColor(activeColor);
            if(ps != null)
            {
                ps.Play();
            }
        }
        else
        {
            UpdateColor(inactiveColor);
            // update particle system clr if it exists
            if (ps != null)
            {
                // lil burst when collided then stop emitting
                ps.Emit(40);
                ps.Stop();
            }
        }
    }




    public void UpdateColor(Color clr)
    {

        // update renderer clr
        GetComponent<Renderer>().material.color = clr;
        
    }

}

