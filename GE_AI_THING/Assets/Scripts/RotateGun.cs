using UnityEngine;

public class RotateGun : MonoBehaviour
{


    public float currentAngle, startAngle;

    // take a guess
    public bool rotating;

    // rotation duration kind f like speed (always same value
    public float rotateDuration;

    // counter until couple seconds
    public float counter;


    // property Things
    private float _xAngle;

    public float xAngle
    {
        get 
        {
            return _xAngle; 
        } 
        set 
        {
            // this gets ran whenever xAngle is changed by another class

            // value is the set input value
            print("uusi kulma annettu; kääntyy kulmaan " + value);


            // set second counter also
            counter = 0;

            startAngle = _xAngle;
            _xAngle = value;

            // when new value is set, start rotating
            rotating = true;

        } 
    } 



    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;

        if (counter > rotateDuration && rotating == true)
        {
            rotating = false;
        }

        currentAngle = Mathf.LerpAngle(startAngle, _xAngle, counter / rotateDuration);
        transform.localEulerAngles = new(currentAngle, 0, 0);
    }
}
