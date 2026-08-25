using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int hits = 0;
    public int totalHits = 0;

    public int score = 0;

    public float timer;

    Player player;

    public bool gameActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(gameActive)
        {
            timer += Time.deltaTime;
            UIManager.Instance.UpdateTimer($"{Mathf.RoundToInt(timer)}s");
        }
            
    }


    public void EndLevel()
    {
        gameActive = false;

        // plr health added as score
        score += player.health;
        // hit count subtracted from score
        score -= hits;


        // remove score as overtime penalty past 30s
        if(timer > 30)
            score -= ((int)timer - 30);
    }

    public void NextLevel()
    {
        totalHits += hits;
        hits = 0;

        StartCoroutine(StageSwitcher());
    }

    IEnumerator StageSwitcher()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        while(SceneManager.GetActiveScene().isLoaded == false)
        {
            yield return null;
        }

        player = FindFirstObjectByType<Player>();

    }

}
