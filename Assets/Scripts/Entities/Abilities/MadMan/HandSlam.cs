using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandSlam : Abilitiy {

    // the actual hand object
    public GameObject Hand;
    // the hand animator
    private Animator HandAnim;

    protected override void Start()
    {
        Hand.SetActive(false);
        HandAnim = Hand.GetComponent<Animator>();

        base.Start();
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    public override void CastAbility()
    {
        base.CastAbility();
        if (GetAbilityUp)
        {
            Hand.SetActive(true);

        }
    }

}
