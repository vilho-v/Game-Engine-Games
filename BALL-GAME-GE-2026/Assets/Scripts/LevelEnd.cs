using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{

    public int levelNumber = 0;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            print("u win");
            GameManager.Instance.EndLevel();
            GameManager.Instance.NextLevel();
        }
    }
}

