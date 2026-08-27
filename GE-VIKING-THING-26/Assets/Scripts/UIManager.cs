using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField]CanvasGroup HUD;
    [SerializeField]CanvasGroup menu;

    [SerializeField] Image healthbar;


    public static UIManager instance;

    Player player;

    float counter, maxcounter;

    float currentFillAmount = 1, targetfillAmount = 1, fillSpeed = 1f;

    public bool coroutineMaxxer = false;


    bool coroutineRunning = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        player = FindFirstObjectByType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (coroutineMaxxer)
            return;













        if(counter > maxcounter)
        {
            counter = 0;
        }
        else
        {
            counter += Time.deltaTime;
        }
    }
    
    public void UpdateHealth(float health, bool instant = false)
    {
        //healthbar.fillAmount = Mathf.Clamp01(player.health / player.maxHealth);
        targetfillAmount = Mathf.Clamp01(health / player.maxHealth);
        if(instant)
        {
            currentFillAmount = targetfillAmount;
            healthbar.fillAmount = currentFillAmount;
        }
        else if (!coroutineRunning)
        {
            StartCoroutine(hp_tweener_thing());
        }

    }

    IEnumerator hp_tweener_thing()
    {
        while(currentFillAmount != targetfillAmount)
        {
            currentFillAmount = Mathf.MoveTowards(currentFillAmount, targetfillAmount, fillSpeed * Time.deltaTime);
            healthbar.fillAmount = currentFillAmount;
            yield return null;
        }

        coroutineRunning = false;
    }
}
