using UnityEngine;

public class throwaway : MonoBehaviour
{

    public int seconds = 5;
    Transform targetTransform;

    void Start()
    {
        Destroy(gameObject, seconds);
        targetTransform = UIManager.instance.player.transform;  
    }

    private void Update()
    {
        if(targetTransform == null)
            return;

        transform.position = targetTransform.position;
    }

}
