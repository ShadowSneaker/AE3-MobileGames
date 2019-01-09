using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.CrossPlatformInput;


public enum EDragDirection { Left, Right, Up, Down, Tap, None }


public class InputScript : MonoBehaviour
{

//#if UNITY_EDITOR
//#elif UNITY_ANDROID
    private Vector3 FirstTouchPos;
    private Vector3 LastTouchPos;


    public int ScreenPercentForSwipe = 5;
    private float DragDistance;

    private Joystick Stick;

    private EDragDirection DragDirection;
//#endif

    private Player User;


    //bool TEMP_ATTACK = true;


    private void Start()
    {
        User = GetComponent<Player>();

//#if UNITY_EDITOR
//#elif UNITY_ANDROID
      DragDistance = Screen.height * ScreenPercentForSwipe / 100;
//#endif
    }


    void FixedUpdate()
    {
//#if UNITY_EDITOR
        


//#elif UNITY_ANDROID
        /// Joystick Inputs

        if (!User.IsDead() && !User.Attacking)
        {
            User.MoveSideways(CrossPlatformInputManager.GetAxisRaw("Horizontal"));


            /// Touch Inputs
            DragDirection = EDragDirection.None;
            if (Input.touchCount == 1)
            {
                Touch To = Input.GetTouch(0);

                switch (To.phase)
                {
                    case TouchPhase.Began:
                        FirstTouchPos = To.position;
                        LastTouchPos = To.position;
                        break;


                    case TouchPhase.Moved:
                        LastTouchPos = To.position;
                        break;


                    case TouchPhase.Ended:
                        LastTouchPos = To.position;
                        CalculateDirection();
                        break;
                }
            }


            User.ActionRegistered(DragDirection);

        }

        /*if (!Player.IsDead())
        {
            Player.MoveSideways(Input.GetAxisRaw("Horizontal"));


            if (Input.GetButtonDown("Jump"))
            {
                Player.Jump();
            }
            
            if (Input.GetKeyDown("q") && TEMP_ATTACK)
            {
                Player.UseAbility(0);
            }
            
            if (Input.GetKeyDown("1"))
            {
                Player.ApplyDamage(1);
            }
        }*/
    }


    private EDragDirection CalculateDirection()
    {
        if (Mathf.Abs(LastTouchPos.x - FirstTouchPos.x) > DragDistance || Mathf.Abs(LastTouchPos.y - FirstTouchPos.y) > DragDistance)
        {
            // Horizontal Swipe | Horizontal > Vertical
            if (Mathf.Abs(LastTouchPos.x - FirstTouchPos.x) > Mathf.Abs(LastTouchPos.y - FirstTouchPos.y))
            {
                if (LastTouchPos.x > FirstTouchPos.x)
                {
                    // Right Swipe
                    DragDirection = EDragDirection.Right;
                }
                else
                {
                    // Left Swipe
                    DragDirection = EDragDirection.Left;
                }
            }
            else
            {
                // Vertical Swipe | Vertical > Horizontal
                if (LastTouchPos.y > FirstTouchPos.y)
                {
                    // Up Swipe
                    DragDirection = EDragDirection.Up;
                }
                else
                {
                    // Down Swipe
                    DragDirection = EDragDirection.Down;
                }
            }
        }
        return DragDirection;
//#endif
    }

//#if UNITY_EDITOR
//#elif UNITY_ANDROID
  public EDragDirection GetDirection()
  {
      return DragDirection;
  }
//#endif
}