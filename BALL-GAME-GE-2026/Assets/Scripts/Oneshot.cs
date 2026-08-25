using UnityEngine;

public class Oneshot : MonoBehaviour
{
    public float duration = 1f;

    private void Start()
    {
        Destroy(gameObject, duration);
    }
}
