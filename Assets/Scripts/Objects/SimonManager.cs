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

    public NoteGuy[] NoteGuys = new NoteGuy[4];


    // The generated pattern to be played.
    private SimonColours[] Pattern;

    private int CurrentStep = 1;
    private int PlayStep = 0;

    private bool Failed;


	// Use this for initialization
	void Start ()
    {
        //GeneratePattern();
        //StartCoroutine(PlaySequence(CurrentStep));
	}


    private IEnumerator PlaySequence(int Amount, float StartDelay = 0.0f)
    {
        Mathf.Clamp(Amount, 0, Pattern.Length);

        yield return new WaitForSeconds(StartDelay);

        for (int i = 0; i < Amount; ++i)
        {
            float NoteDelay = 0.0f;

            //Debug.Log("Played: " + Pattern[i].ToString());
            switch (Pattern[i])
            {
                case SimonColours.Red:
                    NoteGuys[0].Activate();
                    NoteDelay = NoteGuys[0].ShineDuration;
                    break;


                case SimonColours.Green:
                    NoteGuys[1].Activate();
                    NoteDelay = NoteGuys[1].ShineDuration;
                    break;


                case SimonColours.Yellow:
                    NoteGuys[2].Activate();
                    NoteDelay = NoteGuys[2].ShineDuration;
                    break;


                case SimonColours.Blue:
                    NoteGuys[3].Activate();
                    NoteDelay = NoteGuys[3].ShineDuration;
                    break;
            }

            yield return new WaitForSeconds(SequenceDelay + NoteDelay);
        }
    }



    public void GeneratePattern()
    {
        CurrentStep = 1;
        PlayStep = 0;
        Failed = false;
        Pattern = new SimonColours[SegmentCount];
        for (int i = 0; i < SegmentCount; ++i)
        {
            Pattern.SetValue(Random.Range(0, 4), i);
        }

        StartCoroutine(PlaySequence(CurrentStep));
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
            

            if (Pattern[PlayStep] == Col)
            {
                ++PlayStep;

                if (PlayStep >= CurrentStep)
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
                        PlayStep = 0;
                        StartCoroutine(PlaySequence(CurrentStep, 2.0f));
                    }
                }
            }
            else
            {
                Failed = true;

                for (int i = 0; i < NoteGuys.Length; ++i)
                {
                    NoteGuys[i].Activate();
                }
            }
        }
    }
}
