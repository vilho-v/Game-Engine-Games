using UnityEngine;
using UnityEngine.UI;


// all the necessities for ui menu things
public class UIManager : MonoBehaviour
{

    public bool gameActive = false;

    public CanvasGroup menu, game, options;

    bool optionsActive = false;

    [HideInInspector]
    public Player player;

    public static UIManager instance;

    public GameObject imageLoading;

    [SerializeField]
    AudioSource src;

    [SerializeField]
    Slider slider;




    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        } 
        else
        {
            Destroy(gameObject);
            return;
        }




        src.volume = slider.value;
        slider.onValueChanged.AddListener((value) => src.volume = value);

        player = FindFirstObjectByType<Player>();
        

        ToggleCanvasGroup(menu, true);
        ToggleCanvasGroup(game, false);
        ToggleCanvasGroup(options, false);
    }

    // buttons cant call stuff with 2 parameters so i guess just do it HERE!!!
    public void ToggleCanvasGroup (CanvasGroup canvasGroup, bool toggle)
    {
        canvasGroup.alpha = toggle ? 1f : 0f;
        canvasGroup.interactable = toggle;
        canvasGroup.blocksRaycasts = toggle;
    }

    public void StartGame()
    {
        gameActive = true;
        ToggleCanvasGroup(menu, false);
        ToggleCanvasGroup(game, true);

        // rly kind of doesnt matter but i guess its qa for the options to not remain active
        if(optionsActive)
        {
            ToggleCanvasGroup(options, false);
            optionsActive = false;
        }
    }

    public void Menu()
    {
        gameActive = false;
        ToggleCanvasGroup(menu, true);
        ToggleCanvasGroup(game, false);
    }

    public void Options()
    {
        optionsActive = !optionsActive;
        ToggleCanvasGroup(options, optionsActive);
    }
}
