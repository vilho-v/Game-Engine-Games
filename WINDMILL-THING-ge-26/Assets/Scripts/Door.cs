using UnityEngine;

public class Door : MonoBehaviour
{


    Animator anim;

    bool open;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        // tag wud be more efficient i guess
        if(other.TryGetComponent(out PlayerControl pc))
        {
            anim.SetTrigger("DoorTrigger");
        }
    }

    void OnTriggerExit(Collider other)
    {
        
        // yea i guess we cn have the same trigger for both of them
        if(other.TryGetComponent(out PlayerControl pc))
        {
            anim.SetTrigger("DoorTrigger");
        }
    }
}
