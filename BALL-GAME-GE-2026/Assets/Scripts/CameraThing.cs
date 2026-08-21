using UnityEngine;

public class CameraThing : MonoBehaviour
{
    Player player;
    public Vector3 positionOffset;

    void Awake()
    {
        player = FindFirstObjectByType<Player>();   
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + positionOffset;
    }
}
