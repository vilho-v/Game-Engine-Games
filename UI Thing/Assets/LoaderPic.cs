using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoaderPic : MonoBehaviour
{

    float currentFillAmount = 0f, targetFillAmount;
    Image pic;

    void Awake()
    {
        pic = GetComponent<Image>();
        pic.fillAmount = 0f;
    }

    public void Init(float seconds)
    {
        targetFillAmount = 1f;
        StartCoroutine(tween(seconds));
    }

    IEnumerator tween(float seconds)
    {
        float fillSpeed = 1f / seconds;
        
        while (currentFillAmount < targetFillAmount)
        {
            currentFillAmount = Mathf.MoveTowards(currentFillAmount, targetFillAmount, fillSpeed * Time.deltaTime);
            pic.fillAmount = currentFillAmount;
            yield return null;
        }
        Destroy(gameObject);
    }
}
