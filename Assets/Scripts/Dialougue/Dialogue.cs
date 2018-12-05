using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Dialogue
{

    // typically name given to the charecter speaking
    public string name;


    // this creates the area where it can be manipulated in the inspector
    // an array of the dialog the charecter will engage with the NPC
    [TextArea(3, 10)]
    public string[] sentences;


    // this is the replayable sentence (normaly after all main conversations are finished)
    [TextArea(3, 10)]
    public string FinalLine;

    // the opening line an NPC will say when begining talking
    [TextArea(3, 10)]
    public string StartLine;

    // text specified for a NPC that will get angry at skiping
    [TextArea(2, 10)]
    public string[] Angry;
    
}
