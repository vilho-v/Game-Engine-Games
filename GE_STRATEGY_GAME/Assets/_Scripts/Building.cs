using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Building", menuName = "Buildings/Building")]
public class Building : ScriptableObject
{

    // "Listaus" kaikesta informaatiosta = datasta, mikä kuuluu jokaiselle rakennukselle pelissä. 

    public string buildingName;
    public float health;
    public Button menuIcon;
    public Button[] production; // Menu ikonit niille yksiköille, jota rakennus tuottaa
    public GameObject model; 
    public int cost;
    public float buildingDuration;

    public bool buildingInProgress; // Ollaanko juuri rakentamassa rakennusta
    public bool paused;
    public bool done;

    public bool alreadyBuilt;




}
