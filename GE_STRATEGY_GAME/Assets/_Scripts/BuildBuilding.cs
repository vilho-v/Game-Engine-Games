using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;









// tää on kaikissa rakennusnapeissa ja hoitaa napin toiminnallisuuden (alota rakennus, paussaa, jatka yms.)
public class BuildBuilding : MonoBehaviour, IPointerClickHandler
{

    public UnityEvent onLeft, onRight, onMiddle;

    public Image progress;
    public Building building;
    public GameManager gm;
    public float counter;
    public int moneyCounter;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            onLeft.Invoke();
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            onRight.Invoke();
        }
        else if(eventData.button == PointerEventData.InputButton.Middle)
        {
            onMiddle.Invoke();
        }
    }


    void Awake()
    {
        progress = transform.Find("Progress").GetComponent<Image>();
        ResetBuildingValues();
    }

    void Start()
    {
        gm = FindFirstObjectByType<GameManager>();    
        if(building != null)
        {
            building.done = false;
        }
        else
        {
            print("No building assigned to building button: " + gameObject.name);
        }
    }





    void Update()
    {
        if (building.buildingInProgress == true)
        {
            if(gm.money > 0)
            {
                if(counter < building.buildingDuration)
                {
                    counter += Time.deltaTime;
                }

                progress.fillAmount = counter / building.buildingDuration;
                ChangeMoney(building.cost);


                if(counter >= building.buildingDuration)
                {
                    counter = 0;
                    StartCoroutine(BuildingDone());
                }

            }
        }
    }





    // public methods

    // left click
    public void StartBuilding()
    {

        // pause checks
        if(building.paused)
        {
            ResumeBuilding();
            return;
        }

        if (building.buildingInProgress)
        {
            PauseBuilding();
            return;
        }


        // purchase building
        if(gm.money >= building.cost)
        {
            print("Starting building: " + building.buildingName);
            gm.money -= building.cost;
            moneyCounter = building.cost;
            gm.tempMoney += building.cost;

            building.buildingInProgress = true;
            building.paused = false;
            counter = 0f;
        }
        else
        {
            print("Not enough money to build: " + building.buildingName);
        }
    }


    IEnumerator BuildingDone()
    {
        building.buildingInProgress = false;
        print($"Building done: {building.buildingName}, cost: {building.cost}");
        GameObject buildingInstance = Instantiate(building.model, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
        buildingInstance.GetComponent<BuildingManager>().sourceButton = gameObject;
        Camera.main.GetComponent<CameraControl>().draggedBuilding = buildingInstance.GetComponent<BuildingManager>();


        yield return new WaitUntil(() => Camera.main.GetComponent<CameraControl>().draggedBuilding == null);

        building.done = true;
        
        building.paused = false;
        counter = 0f;
        progress.fillAmount = 0;
        moneyCounter = 0;   
    }

    // right click
    public void CancelBuilding()
    {
        if(progress.fillAmount != 0)
        {
            print($"Building canceled: {building.buildingName}, refunded ${moneyCounter}");
            gm.money += building.cost;
            gm.tempMoney -= moneyCounter;
            ResetBuildingValues();
        }
    }

    public void PauseBuilding()
    {
        print($"Building paused: {building.buildingName}, currently paid: {moneyCounter}");
        building.buildingInProgress = false;
        building.paused = true;
    }

    public void ResumeBuilding()
    {
        building.buildingInProgress = true;
        building.paused = false;
    }

    public void ChangeMoney(int amount)
    {
        
        int result = Mathf.RoundToInt(amount * (counter/building.buildingDuration))
            - Mathf.RoundToInt(amount * ((counter - Time.deltaTime)/building.buildingDuration));

        moneyCounter -= result;
        gm.tempMoney -= result;
    }





    public void ResetBuildingValues()
    {
        building.buildingInProgress = false;
        building.paused = false;
        building.done = false;
        counter = 0f;
        progress.fillAmount = 0;
        moneyCounter = 0;
    }
}
