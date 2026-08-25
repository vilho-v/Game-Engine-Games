using UnityEngine;

public class LevelEnd : MonoBehaviour
{

    public int levelNumber = 0;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            print("u win");
        }
    }
}

