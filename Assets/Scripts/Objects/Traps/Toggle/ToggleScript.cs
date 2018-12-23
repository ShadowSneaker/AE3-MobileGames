using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleScript : MonoBehaviour
{
    // Determines if this trap is active or not.
    public bool Active = true;

    public float StartDelay;

    // How long the intervals are between turning on/off.
    public float Duration = 1.0f;

    private Trap BoundTrap;

    private Animator Anim;


    protected virtual void Start()
    {
        Anim = GetComponent<Animator>();
        BoundTrap = GetComponent<Trap>();
        OnToggle();
        StartCoroutine(SwapTimer(StartDelay));
    }


    private IEnumerator SwapTimer(float StartDelay)
    {
        yield return new WaitForSeconds(Duration + StartDelay);
        Toggle();
    }


    private void Toggle()
    {
        Active = !Active;

        if (Anim)
        {
            Anim.SetBool("Play", Active);
        }

        if (BoundTrap)
        {
            BoundTrap.enabled = Active;
        }

        OnToggle();
        StartCoroutine(SwapTimer(0.0f));
    }


    protected virtual void OnToggle()
    {}
}
