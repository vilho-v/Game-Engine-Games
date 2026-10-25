using UnityEngine;

public class Lever : MonoBehaviour, Interactable
{



    public float rotateValue, rotationSpeed = 5;

    public int speedStep = 0;

    public Transform blades;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        blades.Rotate(new(0,0,speedStep * 70 * Time.deltaTime), Space.Self);
        if(rotateValue > 0)
        {
            transform.Rotate(new(0,0,rotationSpeed * Time.deltaTime ), Space.Self);
            rotateValue -= Time.deltaTime * rotationSpeed;
        }
    }

    public void Interact()
    {
        rotateValue += 90;

        speedStep++;

        

        if(speedStep >= 4)
        {
            speedStep = 0;
        }
        else if(speedStep == 3)
        {
            speedStep = 25;
        }
    }
}
