using UnityEngine;

public class Player : MonoBehaviour
{

    public float moveSpeed = 8, rotateSpeed = 25;

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float zMove = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        
        float mouseInput = Input.GetAxis("Mouse X");
        transform.Rotate(new(0,mouseInput,0));


        transform.Translate(xMove, 0 ,zMove);

    }
}
