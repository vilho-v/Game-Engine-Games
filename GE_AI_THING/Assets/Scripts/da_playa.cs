using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class da_playa : MonoBehaviour
{
    public float moveSpeed; // 5
    public float rotateSpeed; // 200
    public CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {

    }


    void Update()
    {
        float xMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float zMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float yMovement = Physics.gravity.y;

        Vector3 moveDirection = new Vector3(xMovement, yMovement, zMovement);
        controller.Move(moveSpeed * Time.deltaTime * transform.TransformDirection(moveDirection));
        //transform.Translate(xMovement, 0, zMovement);

        float mouseInput = Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime;
        Vector3 lookHere = new Vector3(0, mouseInput, 0);
        transform.Rotate(lookHere);

    }


    void OnTriggerEnter(Collider other)
    {

    }
    void OnTriggerExit(Collider other)
    {

    }
}