using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadmanTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    // have a hit box which begins the talking (trigger box)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindObjectOfType<DialogueManager>().BeginDialogue(dialogue);
    }
}
