using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public TextMeshProUGUI healthText, timerText, strokeText;
    public GUIStyle guiStyle = new GUIStyle();

    Player player;
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

        player = FindFirstObjectByType<Player>();
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 300, 40), $"Health: {player.health}/{player.maxHealth}");
        GUI.Label(new Rect(10, 50, 300, 40), $"Velocity: {player.rb.linearVelocity}");
        GUI.Label(new Rect(10, 90, 300, 40), $"Flying: {player.flying}");
        GUI.Label(new Rect(10, 130, 300, 40), $"Damage: {player.damage}");
        GUI.Label(new Rect(10, 170, 300, 40), $"Going down: {player.goingDown}");
        GUI.Label(new Rect(10, 210, 300, 40), $"High point: {player.highPoint}");

    }

    public void UpdateHealth(string input)
    {
        // Update the health UI here
     
        healthText.text = input;
    }

    public void UpdateTimer(string input)
    {
        timerText.text = input;
    }

    public void UpdateStrokes(string input)
    {
        strokeText.text = input;
    }
}
