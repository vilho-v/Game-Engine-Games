using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed; // 5
    public float rotateSpeed; // 200
    public CharacterController controller;

    public Interactable nearbyStation;

    // Start is called before the first frame update
    void Start()
    {
       
    }

  
    void LateUpdate()
    {
        float xMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float zMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float yMovement = Physics.gravity.y;
        Vector3 moveDirection = new Vector3(xMovement, yMovement, zMovement);
        controller.Move(moveDirection * Time.deltaTime);
        transform.Translate(xMovement, 0, zMovement);

        float mouseInput = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        Vector3 lookHere = new Vector3(0, mouseInput, 0);
        transform.Rotate(lookHere);

        // whenn near a thing and a btn is pressed it will be interacted with
        if(nearbyStation != null && Input.GetKeyDown(KeyCode.Space))
        {
            nearbyStation.Interact();
        }


    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Interactable it))
        {
            nearbyStation = it;
        }
    }
    private void OnTriggerExit(Collider other)
    {

    }
}
