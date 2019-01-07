using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Abilitiy
{
    // The speed the entity dashes
    public float DashSpeed;

    public bool UseDamageRange;
    public int Damage;
    public int MaxDamage;

    // How long the dash takes.
    public float DashDuration;

    // Should this object dash in the direction it is facing?
    public bool DashForward = true;

    // Dashes in a specific direction, if Dash Forward is enabled this is ignored.
    public Vector2 DashDirection;

    public bool ReturnOnEnd;

    public float WaitDelay = 0.5f;

    public bool TurnOnEnd;

    public bool StunOnWallHit;
    public float StunDuration;


    protected Entity This;


    private Vector3 StartPos;

    private bool EnableTimer;

    private float Timer;

    private Rigidbody2D Rigid;

    private bool StartReturn = false;

    private int StartDir;

    private int CurrentDir;



	// Use this for initialization
	protected override void Start ()
    {
        base.Start();
        Rigid = GetComponent<Rigidbody2D>();
        This = GetComponent<Entity>();

        if (ReturnOnEnd)
        {
            StartPos = transform.position;
            
        }
        StartDir = (transform.localScale.x > 0.0f) ? 1 : -1;
        CurrentDir = StartDir;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (ReturnOnEnd && StartReturn && !EnableTimer)
        {
            int Direction;
            Direction = (transform.position.x < StartPos.x) ? 1 : -1;
            This.MoveSideways(Direction);
            StartDir = Direction;
            CurrentDir = Direction;

            if ((Direction < 0 && transform.position.x - 0.01 <= StartPos.x) || (Direction > 0 && transform.position.x + 0.01 >= StartPos.x))
            {
                This.MoveSideways(0);
                StartReturn = false;
                StartCoroutine(StartCountdown());
            }
        }
        else
        {
            if (TurnOnEnd)
            {
                if (StartDir == CurrentDir)
                {
                    CurrentDir = -CurrentDir;
                    transform.localScale = new Vector3(CurrentDir, transform.localScale.y, transform.localScale.z);
                }
            }
            else
            {
                transform.localScale = new Vector3(This.transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }

        if (EnableTimer)
        {
            if (Timer > 0.0f)
            {
                Timer -= Time.deltaTime;
                
                Rigid.velocity = ((DashForward) ? new Vector2(transform.localScale.x, 0.0f) : DashDirection) * DashSpeed * Time.deltaTime;
            }
            else
            {
                EnableTimer = false;
            }
        }
	}


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Entity Other = collision.gameObject.GetComponent<Entity>();
        if (Other && EnableTimer)
        {
            Other.ApplyDamage((UseDamageRange) ? Random.Range(Damage, MaxDamage) : Damage, This);
        }
        else if (StunOnWallHit && collision.gameObject.CompareTag("Untagged"))
        {
            StartCoroutine(This.StartStunned(StunDuration));
        }
    }


    public override void CastAbility()
    {
        base.CastAbility();

        Timer = DashDuration;
        EnableTimer = true;
    }


    public override void EndAbility()
    {
        Anim.SetBool("Attack", false);
        Anim.SetLayerWeight(2, 0.0f);

        if (ReturnOnEnd)
        {
            StartCoroutine(ReturnDelayTimer());
            This.Attacking = false;
        }
        else
        {
            StartCoroutine(StartCountdown());
        }
    }


    private IEnumerator ReturnDelayTimer()
    {
        yield return new WaitForSeconds(WaitDelay);
        StartReturn = true;
    }

}
