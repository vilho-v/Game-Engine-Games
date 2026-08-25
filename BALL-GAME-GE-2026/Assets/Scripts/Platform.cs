using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{
    Player player;
    Renderer rend;
    [SerializeField] Color activeColor;
    Color inactiveColor;


    bool active = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rend = GetComponent<Renderer>();
        player = FindFirstObjectByType<Player>();

        //all platforms are inactive when the game starts
        inactiveColor = rend.material.color;
        gameObject.layer = LayerMask.NameToLayer("PlatformInactive");
        
    }

    /// Update is called once per frame
    //void Update()
    //{
    //    if(player.transform.position.y > transform.position.y + transform.localScale.y)
    //    {
    //        TogglePlatform(true);
    //    }
    //}

    public void TogglePlatform(bool toggle)
    {
        if(active == toggle) return;
        active = toggle;

        if (toggle)
        {
            gameObject.layer = LayerMask.NameToLayer("Platform");
            rend.material.color = activeColor;
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("PlatformInactive");
            rend.material.color = inactiveColor;
        }
    }
}
