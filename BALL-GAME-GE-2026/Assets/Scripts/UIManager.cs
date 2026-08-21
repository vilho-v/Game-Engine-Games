using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TextMeshProUGUI healthText;

    void Awake()
    {

        // single ... ton
        if (Instance == null)
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
        
    }


    public void UpdateHealth(int health)
    {
        // Update the health UI here
        Debug.Log("Health updated to: " + health);
        healthText.text = "Health: " + health;
    }
}
