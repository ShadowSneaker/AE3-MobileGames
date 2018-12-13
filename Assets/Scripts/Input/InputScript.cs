using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.CrossPlatformInput;


public enum EDragDirection { Left, Right, Up, Down }


public class InputScript : MonoBehaviour
{

#if UNITY_EDITOR
#elif UNITY_ANDROID
    private Vector3 FirstTouchPos;
    private Vector3 LastTouchPos;


    public int ScreenPercentForSwipe = 5;
    private float DragDistance;

    public UnityEvent DragUp;
    public UnityEvent DragDown;
    public UnityEvent DragLeft;
    public UnityEvent DragRight;
    public UnityEvent Pressed;
    public UnityEvent Dragging;
    public UnityEvent Released;

    private Joystick Stick;

    private EDragDirection DragDirection;
#endif

    private Entity Player;



    bool TEMP_ATTACK = true;


    private void Start()
    {
        Player = GetComponent<Entity>();

#if UNITY_EDITOR
#elif UNITY_ANDROID
        DragDistance = Screen.height * ScreenPercentForSwipe / 100;
#endif
    }


    void Update()
    {
#if UNITY_EDITOR
        if (!Player.IsDead())
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
        }


#elif UNITY_ANDROID
        /// Joystick Inputs

        if (!Player.IsDead())
        {
            Player.MoveSideways(CrossPlatformInputManager.GetAxis("Horizontal"));


            /// Touch Inputs

            if (Input.touchCount == 1)
            {
                Touch To = Input.GetTouch(0);

                switch (To.phase)
                {
                    case TouchPhase.Began:
                        FirstTouchPos = To.position;
                        LastTouchPos = To.position;
                        if (Pressed != null)
                        {
                            Pressed.Invoke();
                        }

                        break;


                    case TouchPhase.Moved:
                        LastTouchPos = To.position;



                        CalculateDirection();



                        if (Dragging != null)
                        {
                            Dragging.Invoke();
                        }



                        break;


                    case TouchPhase.Ended:
                        LastTouchPos = To.position;

                        if (Released != null)
                        {
                            Released.Invoke();
                        }

                        CalculateDirection();

                        switch (DragDirection)
                        {
                            case EDragDirection.Left:
                                if (DragLeft != null)
                                {
                                    DragLeft.Invoke();
                                }
                                break;


                            case EDragDirection.Right:
                                if (DragRight != null)
                                {
                                    DragRight.Invoke();
                                }
                                break;


                            case EDragDirection.Up:
                                if (DragUp != null)
                                {
                                    DragUp.Invoke();
                                }
                                break;


                            case EDragDirection.Down:
                                if (DragDown != null)
                                {
                                    DragDown.Invoke();
                                }
                                break;
                        }

                        break;
                }
            }
        }

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
#endif
    }

#if UNITY_EDITOR
#elif UNITY_ANDROID
    public EDragDirection GetDirection()
    {
        return DragDirection;
    }
#endif
}