using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{
    Player player;

    
    bool active = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<Player>();

        // set layer to inactive on start , if plr already above it, gets set to active
        gameObject.layer = LayerMask.NameToLayer("PlatformInactive");
    }

    // Update is called once per frame
    void Update()
    {
        // stop checking for plr position when already activated
        if(active)
            return;

        if(player.transform.position.y > transform.position.y + transform.localScale.y)
        {
            active = true;
            gameObject.layer = LayerMask.NameToLayer("Platform");
        }
    }
}
