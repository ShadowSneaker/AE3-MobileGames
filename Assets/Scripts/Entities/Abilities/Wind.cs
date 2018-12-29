using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : Abilitiy
{
    public bool EffectSelf;
    public float Duration;
    public float Strength;

    public int Direction;

    private Entity[] Entities;
    private bool EnableTime;
    private float CurrentTime;
    private Entity This;

	// Use this for initialization
	protected override void Start ()
    {
        Entities = FindObjectsOfType<Entity>();
        This = GetComponent<Entity>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (EnableTime)
        {
            if (CurrentTime > 0.0f)
            {
                CurrentTime -= Time.deltaTime;
                for (int i = 0; i < Entities.Length; ++i)
                {
                    if (EffectSelf)
                    {
                        Entities[i].Rigid.AddForce(new Vector2(Direction * Strength * Time.deltaTime, 0.0f));
                    }
                    else
                    {
                        if (Entities[i] != This)
                        {
                            Entities[i].Rigid.AddForce(new Vector2(Direction * Strength * Time.deltaTime, 0.0f));
                        }
                    }
                }
            }
            else
            {
                EnableTime = false;
            }
        }
	}


    public override void CastAbility()
    {
        base.CastAbility();
        EnableTime = true;
        CurrentTime = Duration;
    }
}
