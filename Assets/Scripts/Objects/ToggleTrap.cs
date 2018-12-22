using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTrap : Trap
{
    // Determines if this trap is active or not.
    public bool Active = true;

    // How long the intervals are between turning on/off.
    public float Duration = 1.0f;


    // A reference to a particle system attached (if any).
    private ParticleSystem Particle;




    private void Start()
    {
        Particle = GetComponent<ParticleSystem>();
        if (Particle && Active)
        {
            Particle.Play();
        }

        StartCoroutine(SwapTimer());
    }


    protected override void FixedUpdate ()
    {
        if (Active)
        {
            base.FixedUpdate();
        }
	}


    private IEnumerator SwapTimer()
    {
        yield return new WaitForSeconds(Duration);
        Swap();
    }


    private void Swap()
    {
        Active = !Active;

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

        StartCoroutine(SwapTimer());
    }
}
