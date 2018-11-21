﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilitiy : MonoBehaviour {

    // How long the ability takes to be active again (Counts in seconds);
    public float Cooldown = 5f;
    private bool AbilityUp = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
	}


    public IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(Cooldown);

        AbilityUp = true;
    }


    public virtual void CastAbility()
    {
        if (AbilityUp)
        {
            AbilityUp = false;
            StartCoroutine(StartCountdown());
        }
    }
}
