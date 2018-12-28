using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destructable : Entity
{

    private ParticleSystem Particle;
    private BoxCollider2D Box;
    private MeshRenderer Rend;

    public UnityEvent DestroyEvent;

    protected override void Start()
    {
        base.Start();
        Box = GetComponent<BoxCollider2D>();
        Particle = GetComponent<ParticleSystem>();
        Rend = GetComponent<MeshRenderer>();
    }


    public override void OnDeath()
    {
        Rend.enabled = false;
        Box.enabled = false;
        Particle.Play();

        if (DestroyEvent != null)
        {
            DestroyEvent.Invoke();
        }
    }
}
