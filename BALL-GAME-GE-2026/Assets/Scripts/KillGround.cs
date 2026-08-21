using UnityEngine;

public class KillGround : MonoBehaviour
{
    public int damage = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Player player))
        {
            player.Damage(damage);
        }
    }
}

