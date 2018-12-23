using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTrap : ToggleScript
{
    // A reference to a particle system attached (if any).
    private ParticleSystem Particle;



    protected override void Start()
    {
        Particle = GetComponent<ParticleSystem>();
        if (Particle && Active)
        {
            Particle.Play();
        }

        base.Start();
    }


    protected override void OnToggle()
    {
        if (Particle)
        {
            if (Active)
            {
                Particle.Play();
            }
            else
            {
                Particle.Stop();
            }
        }
    }
}
