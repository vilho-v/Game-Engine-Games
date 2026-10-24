using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class Player : MonoBehaviourPunCallbacks
{

    public float moveSpeed = 8, rotateSpeed = 25;


    public Camera cam;
    public GameObject ammo, barrel;

    public float health;
    public string playerName;

    public TextMeshPro healthText, nameText;

    void Start()
    {

        // this checks if the critter in question is the photonview client's own
        // if tru then enable this critters cam
        if(photonView.IsMine)
        {
            cam.enabled = true;

            // poistaako tämä sen kaikilta?
            GameObject.FindGameObjectWithTag("BirdView").GetComponent<Camera>().enabled = false;
        }

        // if untrue then disable this critters cam so that the new client uses that character's cam
        else
        {
            cam.enabled = false;
        }

        nameText.text = playerName.ToString();
    }
    // Update is called once per frame
    void Update()
    {

        healthText.SetText(health.ToString());


        if(photonView.IsMine)
        {
            float xMove = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
            float zMove = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            
            float mouseInput = Input.GetAxis("Mouse X");
            transform.Rotate(new(0,mouseInput*rotateSpeed,0));


            transform.Translate(xMove, 0 ,zMove);

            if(Input.GetButtonDown("Fire1"))
            {
                photonView.RPC("Shoot", RpcTarget.All);
            }
        }

    }


    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ammo"))
        {
            if(photonView.IsMine)
            {
                // Use rpc to call losehealth so all game instances see the lowered health value
                photonView.RPC("LoseHealth", RpcTarget.AllBuffered);
            }
            Destroy(collision.gameObject);
        }
    }

    [PunRPC]
    public void LoseHealth()
    {
        health -= 15;
    }

    [PunRPC]
    public void Shoot()
    {
        GameObject bullet = Instantiate(ammo, barrel.transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(barrel.transform.forward * 20, ForceMode.Impulse);
        Destroy(bullet, 3);
    }
}




