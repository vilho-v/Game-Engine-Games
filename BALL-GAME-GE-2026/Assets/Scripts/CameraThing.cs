using UnityEngine;

public class CameraThing : MonoBehaviour
{
    Player player;
    public Vector3 positionOffset;

    public bool smooth;

    public float cameraDamping = 0.05f;
    void Awake()
    {
        player = FindFirstObjectByType<Player>();   
    }

    // Update is called once per frame
    Vector3 vel = Vector3.zero;
    void Update()
    {
        if(!smooth)
        {
            transform.position = player.transform.position + positionOffset;
        }
        else
        {
            transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + positionOffset, ref vel, cameraDamping);
        }
    }
}
