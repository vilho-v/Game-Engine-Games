using UnityEngine;
using System.Collections;


// enemy which shoots at plr when targeted
public class TurretEnemy : MonoBehaviour
{

    MeshRenderer indicator;

    public GameObject targetObject;
    public Transform ammoSpawn;
    public RotateGun gunRotator;
    public float force;

    public GameObject bullet;

    public Vector3 gravity;

    public int angleMultiplier;

    private void Awake()
    {
        indicator = transform.GetChild(0).GetComponent<MeshRenderer>();

        indicator.material.color = Color.white;

        gravity = Physics.gravity;
    }



    public void Shoot(Transform target)
    {
        if(gunRotator.rotating)
        {
            print("pyssy on ampumassa edelleen; ei ammuta vielä");
            return;
        }
        StartCoroutine(ShootBalls(target));
    }
    public IEnumerator ShootBalls(Transform target)
    {
        indicator.material.color = Color.red;
        print($"arg i be shootin at {target.name} t. {gameObject.name}" );


        Vector3 targetPos = target.position - transform.position;
        Quaternion targetDir = Quaternion.LookRotation(targetPos, Vector3.up);
        var startRotation = transform.rotation;


        while(true)
        {
            Quaternion.Lerp(startRotation, targetDir, 1);

            if (transform.eulerAngles.y == targetDir.eulerAngles.y) 
                break;
        }

        // get direction of shots
        Vector3[] direction = hitTargetBySpeed(ammoSpawn.position, target.position, gravity, force);


        if(gameObject.transform.position.z < target.transform.position.z)
        {
            angleMultiplier = -1;
        }
        else
        {
            angleMultiplier = 1;
        }

            foreach (Vector3 dir in direction)
            {
                float ang = Mathf.Atan(dir.y / dir.z) * Mathf.Rad2Deg * angleMultiplier;
                // point gun via RotateGun
                print("piiipun pitäisi kääntyä kulmaan " + ang);
                gunRotator.xAngle = ang;

                yield return new WaitUntil(() => gunRotator.rotating == false);

                // shoot
                GameObject projectile = Instantiate(bullet, ammoSpawn.position, Quaternion.identity);
                projectile.GetComponent<Rigidbody>().AddRelativeForce(dir, ForceMode.Impulse);

                yield return new WaitForSeconds(1);
            }




        // shoot
        //GameObject projectile = Instantiate(bullet, ammoSpawn.position, Quaternion.identity);
        //projectile.GetComponent<Rigidbody>().AddRelativeForce(direction[0], ForceMode.Impulse);

        //yield return new WaitForSeconds(1);

        //GameObject projectile2 = Instantiate(bullet, ammoSpawn.position, Quaternion.identity);
        //projectile2.GetComponent<Rigidbody>().AddRelativeForce(direction[1], ForceMode.Impulse);

        //indicator.material.color = Color.white;
    }





    public Vector3[] hitTargetBySpeed(Vector3 startPosition, Vector3 targetPosition, Vector3 gravityBase, float launchForce)
    {

        Vector3 ab = targetPosition - startPosition;    

        Vector3 horizontal = GetHorizontalVector(ab, gravityBase, startPosition);
        float horizontalDistance = horizontal.magnitude;   

        Vector3 vertical = GetVerticalVector(ab, gravityBase, startPosition);
        float verticalDistance = vertical.magnitude * Mathf.Sign(Vector3.Dot(vertical, -gravityBase));


        // do a launchtest 
        // if pos can hit target at some angle
        // if negative cant hit even at 45 degrees

        float x2 = horizontalDistance * horizontalDistance;
        float v2 = launchForce * launchForce; 
        float v4 = launchForce * launchForce * launchForce * launchForce;  
        float gravMag = gravityBase.magnitude;

        // v4 - (g * ((g * x^2) + (2 * y * v^2))) >= 0
        
        //float launchTest = v4 - (gravMag * ((gravMag * x2) + (2 * verticalDistance*v2)));
        
        float launchTest = v4 - (gravMag * ((gravMag * x2) + (2 * verticalDistance*v2)));

        print("launchTest: " + launchTest);

        Vector3[] launch = new Vector3[2];

        if (launchTest < 0)
        {
            print("cant hit but will shoot anyway twice at 45 deg");

            launch[0] = (horizontal.normalized * launchForce * Mathf.Cos(Mathf.Deg2Rad * 45))
                - (gravityBase.normalized * launchForce * Mathf.Sin(Mathf.Deg2Rad * 45));

            launch[1] = (horizontal.normalized * launchForce * Mathf.Cos(Mathf.Deg2Rad * 45))
                - (gravityBase.normalized * launchForce * Mathf.Sin(Mathf.Deg2Rad * 45));
        }

        else
        {
            print("can hit target. calculatin angles");
            float[] tanAngle = new float[2]; // need 2 angles to hit target at same speed. one is high arc, other is low arc

            tanAngle[0] = (v2 - Mathf.Sqrt(launchTest)) / (gravMag * horizontalDistance);
            tanAngle[1] = (v2 + Mathf.Sqrt(launchTest)) / (gravMag * horizontalDistance);

            float[] finalAngle = new float[2];

            finalAngle[0] = Mathf.Atan(tanAngle[0]);
            finalAngle[1] = Mathf.Atan(tanAngle[1]);

            print("finalAngle[0]: " + finalAngle[0] * Mathf.Rad2Deg + "finalAngle[1]: " + finalAngle[1] * Mathf.Rad2Deg);


            launch[0] = (horizontal.normalized * launchForce * Mathf.Cos(finalAngle[0]))
                - (gravityBase.normalized * launchForce * Mathf.Sin(finalAngle[0]));

            launch[1] = (horizontal.normalized * launchForce * Mathf.Cos(finalAngle[1]))
                - (gravityBase.normalized * launchForce * Mathf.Sin(finalAngle[1]));
        }


            return launch;
    }

    Vector3 GetHorizontalVector(Vector3 ab, Vector3 gravityBase, Vector3 startPosition)
    {
        Vector3 output = new();


        Vector3 perpendicular = Vector3.Cross(ab, gravityBase);
        perpendicular = Vector3.Cross(gravityBase, perpendicular);
        output = Vector3.Project(ab, perpendicular);

        Debug.DrawRay(startPosition, output, Color.red, 3);

        return output;
    }

    Vector3 GetVerticalVector(Vector3 ab, Vector3 gravityBase, Vector3 startPosition)
    {
        Vector3 output = new();

        output = Vector3.Project(ab, gravityBase);

        Debug.DrawRay(startPosition, output, Color.blue, 3);  

        return output;
    }

}
