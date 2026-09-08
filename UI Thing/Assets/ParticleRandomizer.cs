using UnityEditor.Toolbars;
using UnityEngine;

public class ParticleRandomizer : MonoBehaviour
{

    ParticleSystem ps;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();

        Randomize();
    }
    

    public void Randomize()
    {
        var main = ps.main;

        // random clr
        main.startColor = new ParticleSystem.MinMaxGradient(Random.ColorHSV());
        main.startSize = Random.Range(0.1f, 1f);
        main.startSpeed = Random.Range(0.1f, 5f);

        main.startRotation = Random.Range(0f, 360f);

        main.simulationSpeed = Random.Range(0.1f, 2f);
        main.flipRotation = Random.Range(0, 360);

        main.startLifetime = Random.Range(0.1f, 5f);
        main.emitterVelocity = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f));

        main.startRotationMultiplier = Random.Range(0f, 5f);

        main.gravityModifier = Random.Range(-5f, 5f);

        main.startDelay = Random.Range(0f, 2f);

        main.simulationSpeed = Random.Range(0.5f, 2f);

        main.

        ps.Play();

    }
}
