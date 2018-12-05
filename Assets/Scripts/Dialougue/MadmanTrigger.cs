using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadmanTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    // have a hit box which begins the talking (trigger box)


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // this could be changed to a variable above
        FindObjectOfType<DialogueManager>().BeginDialogue(dialogue);
        FindObjectOfType<DialogueManager>().GetFinalText(dialogue.FinalLine);
    }
}
