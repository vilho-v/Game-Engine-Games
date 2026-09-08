using UnityEngine;

public class Player : MonoBehaviour
{

    UIManager ui;

    Rigidbody2D rb;
    [SerializeField]
    float moveSpeed = 10, maxSpeed = 10, jumpForce = 5;


    void Awake()
    {
        ui = FindFirstObjectByType<UIManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ui.gameActive)
            return;



        if (Input.GetAxisRaw("Horizontal") != 0)
        {

            if (rb.linearVelocity.magnitude < maxSpeed)
                //rb.AddForce(Input.GetAxis("Horizontal") * Vector2.right * moveSpeed * Time.deltaTime, ForceMode2D.Impulse);
                //rb.AddForce(Input.GetAxis("Horizontal") * Vector2.right * moveSpeed, ForceMode.VelocityChange);
                rb.linearVelocity = Vector3.MoveTowards(rb.linearVelocity, new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.linearVelocity.y, 0), moveSpeed * Time.deltaTime);

            // move position RUINS inertia !!!
            //rb.MovePosition(transform.position + Input.GetAxisRaw("Horizontal") * Vector3.right * moveSpeed * Time.deltaTime);
            transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), 1, 1);
           
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
