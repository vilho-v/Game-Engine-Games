using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject sourceButton;
    public Collider basePlate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        basePlate = GetComponentInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
