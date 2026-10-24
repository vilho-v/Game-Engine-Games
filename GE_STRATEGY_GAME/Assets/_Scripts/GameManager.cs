using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int money, tempMoney;
    public TextMeshProUGUI moneyText;
    public Building[] buildings; // Array kaikista rakennuksista, jotka peli sisältää. näistä tehdään nappulat valikkoon

    public Canvas menu;

    void Start()
    {
        foreach(Building bld in buildings)
        {
            Button buildingButton = Instantiate(bld.menuIcon);
            buildingButton.transform.SetParent(menu.transform.GetChild(0));
        }   
    }

    void Update()
    {
        if(tempMoney <= 0)
            moneyText.text = "Money: " + money.ToString();
        else
            moneyText.text = "Money: " + money.ToString() + " (+" + tempMoney.ToString() + ")";
    } 
}
