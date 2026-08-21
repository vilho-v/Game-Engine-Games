using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{
    Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.transform.position.y > transform.position.y)
        {
            gameObject.layer = LayerMask.NameToLayer("PlatformInactive");
        }
    }
}
