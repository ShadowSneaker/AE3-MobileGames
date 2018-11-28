using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class DialogueManager : MonoBehaviour
{
    // creates a que of sentences that the npc can iterate through
    private Queue<string> senteces;
    
    public PostProcessingProfile Blur;
    public PostProcessingProfile Default;


    public Animator Anim;
    public Animator PlayerAnim;
    public Animator NPCAnim;

	void Start ()
    {
        senteces = new Queue<string>();

	}


    public void BeginDialogue(Dialogue dialogue)
    {
        //set animation bool
        Anim.SetBool("DialogStart", true);
        PlayerAnim.SetBool("Fader", true);
        NPCAnim.SetBool("Fader", true);
        // still need to set the bools to false in a later function

        // disable player input
        // something needs to be put in place

        // create a blur on the background
        FindObjectOfType<Camera>().GetComponent<PostProcessingBehaviour>().profile = Blur;
        
    }
}
