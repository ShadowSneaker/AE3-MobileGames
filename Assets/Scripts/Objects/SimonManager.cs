using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum SimonColours
{
    Red = 0,
    Green = 1,
    Yellow = 2,
    Blue = 3
}


public class SimonManager : MonoBehaviour
{


    // How many steps there are in the pattern.
    public int SegmentCount = 7;

    // The time between each note played.
    public float SequenceDelay = 0.5f;

    public UnityEvent OnCompleted;


    // The generated pattern to be played.
    private SimonColours[] Pattern;

    private int CurrentStep = 1;

    private bool Failed;


	// Use this for initialization
	void Start ()
    {
        GeneratePattern();
        StartCoroutine(PlaySequence(CurrentStep));
	}


    private IEnumerator PlaySequence(int Amount)
    {
        Mathf.Clamp(Amount, 0, Pattern.Length);

        for (int i = 0; i < Amount; ++i)
        {
            Debug.Log("Played: " + Pattern[i].ToString());
            switch (Pattern[i])
            {
                case SimonColours.Red:
                    break;


                case SimonColours.Green:

                    break;


                case SimonColours.Yellow:

                    break;


                case SimonColours.Blue:

                    break;
            }

            yield return new WaitForSeconds(SequenceDelay);
        }
    }



    public void GeneratePattern()
    {
        CurrentStep = 1;
        Failed = false;
        Pattern = new SimonColours[SegmentCount];
        for (int i = 0; i < SegmentCount; ++i)
        {
            Pattern.SetValue(Random.Range(0, 4), i);
        }
    }


    
    public void Input(int Colour)
    {
        if (!Failed)
        {
            // For some reason I can't just use a SimonColours enum for a parameter without having to set a property on the other object.
            SimonColours Col = SimonColours.Red;
            switch (Colour)
            {
                case 0:
                    Col = SimonColours.Red;
                    break;

                case 1:
                    Col = SimonColours.Green;
                    break;

                case 2:
                    Col = SimonColours.Yellow;
                    break;

                case 3:
                    Col = SimonColours.Blue;
                    break;
            }


            if (Pattern[CurrentStep] == Col)
            {
                ++CurrentStep;
                if (CurrentStep > Pattern.Length)
                {
                    if (OnCompleted != null)
                    {
                        OnCompleted.Invoke();
                    }
                }
                else
                {
                    PlaySequence(CurrentStep);
                }
            }
            else
            {
                Failed = true;
            }
        }
    }
}
