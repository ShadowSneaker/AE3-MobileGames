using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Dialogue
{

    // typically name given to the charecter speaking
    public string name;


    // this creates the area where it can be manipulated in the inspector
    [TextArea(3, 10)]
    public string[] sentences;


    // i want this to be the replay sentence always said
    [TextArea(3, 10)]
    public string FinalLine;
}
