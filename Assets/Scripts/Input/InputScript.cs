using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public enum EDragDirection { Left, Right, Up, Down }


public class InputScript : MonoBehaviour
{

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

    //public UnityEvent Tap;

    private EDragDirection DragDirection;


    private void Start()
    {
        DragDistance = Screen.height * ScreenPercentForSwipe / 100;
    }


    void Update()
    {
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
                    //if (DragRight != null)
                    //{
                    //    DragRight.Invoke();
                    //}
                }
                else
                {
                    // Left Swipe
                    DragDirection = EDragDirection.Left;
                    //if (DragLeft != null)
                    //{
                    //    DragLeft.Invoke();
                    //}
                }
            }
            else
            {
                // Vertical Swipe | Vertical > Horizontal
                if (LastTouchPos.y > FirstTouchPos.y)
                {
                    // Up Swipe
                    DragDirection = EDragDirection.Up;
                    //if (DragUp != null)
                    //{
                    //    DragUp.Invoke();
                    //}
                }
                else
                {
                    // Down Swipe
                    DragDirection = EDragDirection.Down;
                    //if (DragDown != null)
                    //{
                    //    DragDown.Invoke();
                    //}
                }
            }
        }
        else
        {
            // Tap

        }
        return DragDirection;
    }


    public EDragDirection GetDirection()
    {
        return DragDirection;
    }
}
