using UnityEngine;

public class ParticleRandomizer : MonoBehaviour
{

    // particle sys component
    ParticleSystem ps;


    // all the random mats it can be assigned (used for color n things)
    [SerializeField]
    Material[] mats;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();

        Randomize();
    }
    

    public void Randomize()
    {
        // each of these is one of the tabs in a particle system
        // use var so no need to fudge around with specific names 

        // this has all the basic things
        var main = ps.main;

        // trails
        var trails = ps.trails;

        // control the initial shape which particles come out of
        var shape = ps.shape;

        var emission = ps.emission;
        // this stupid thing is part of a particle system yet its treated as a component
        var rend = GetComponent<ParticleSystemRenderer>();

        // randomize main things
        main.startColor = new ParticleSystem.MinMaxGradient(Random.ColorHSV());
        main.startSize = Random.Range(0.1f, 1f);
        main.startSpeed = Random.Range(0.1f, 5f);

        main.startRotation = Random.Range(0f, 360f);

        main.simulationSpeed = Random.Range(0.1f, 2f);
        main.flipRotation = Random.Range(0, 360);

        main.startLifetime = Random.Range(0.1f, 5f);
        main.emitterVelocity = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), Random.Range(-2f, 2f));

        main.startRotationMultiplier = Random.Range(0f, 5f);

        main.gravityModifier = Random.Range(-5f, 5f);

        main.startDelay = Random.Range(0f, 0.8f);

        main.simulationSpeed = Random.Range(0.3f, 3f);

        // RENDERER
        rend.material = mats[Random.Range(0, mats.Length)];
        
        rend.trailMaterial = mats[Random.Range(0, mats.Length)];


        //TRAIL
        trails.lifetimeMultiplier = Random.Range(-0.5f,1f);
        trails.widthOverTrail = Random.Range(0.3f, 1);
        
        emission.rateOverTime = Random.Range(1,100);

        

        // random size for the circle which spawns em
        shape.radius = Random.Range(0.4f, 3f);

        // new burst with random params (dont set one in editor)
        ParticleSystem.Burst burst = new(_time: Random.Range(0,1), _count: Random.Range(1,100), 
        _repeatInterval: Random.Range(0.001f, 0.5f), _cycleCount: Random.Range(1,3));

        emission.SetBurst(0, burst);




        ps.Play();

    }
}
