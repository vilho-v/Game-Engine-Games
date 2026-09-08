using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ParticleButton : MonoBehaviour
{

    public GameObject particlePrefab;

    GameObject picThing;

    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
    }


    // spawn particle and temporarily disable btn
    public void SpawnParticle()
    {
        if (particlePrefab == null)
            return;


        Instantiate(particlePrefab, UIManager.instance.player.transform.position, Quaternion.identity);
        picThing = Instantiate(UIManager.instance.imageLoading, transform.position, Quaternion.identity);
        StartCoroutine(koroutne());
        
    }



    // this waits until the loading thing goes away and then re enables button
    IEnumerator koroutne()
    {
        button.interactable = false;

        yield return new WaitUntil(() => picThing == null);

        button.interactable = true;
    }
}
