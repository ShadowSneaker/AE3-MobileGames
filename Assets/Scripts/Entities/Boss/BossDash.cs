using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDash : Abilitiy
{
    private Entity This;

    private Emjieph Boss;

    private GameObject[] Walls;

    private int CurrentIndex = 0;

    private int Direction;

	// Use this for initialization
	protected override void Start ()
    {
        This = GetComponent<Entity>();
        Walls = GameObject.FindGameObjectsWithTag("BossWall");
        Boss = GetComponent<Emjieph>();
	}


    private void Update()
    {
        This.MoveSideways(Direction);
    }


    public override void CastAbility()
    {
        base.CastAbility();
        
           Direction = (CurrentIndex == 0) ? 1 : -1;
    }


    public override void EndAbility()
    {
        //StartCoroutine(StartCountdown());
        if (Anim)
        {
            Anim.SetBool("Attack", false);
            Anim.SetLayerWeight(2, 0.0f);
        }
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Walls[0])
        {
            CurrentIndex = 1;
            Direction = 0;
            StartCoroutine(TurnAround());
        }
        else if (collision.gameObject == Walls[1])
        {
            CurrentIndex = 0;
            Direction = 0;
            StartCoroutine(TurnAround());
        }
    }


    private IEnumerator TurnAround()
    {
        StartCoroutine(StartCountdown());
        Boss.AbilityFinished(0.0f);
        yield return new WaitForSeconds(0.1f);
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
