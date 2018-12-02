using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // creates a que of sentences that the npc can iterate through
    private Queue<string> Senteces;
    
    public PostProcessingProfile Blur;
    public PostProcessingProfile Default;


    public Animator Anim;
    public Animator PlayerAnim;
    public Animator NPCAnim;

    public Button SkipB;

	void Start ()
    {
        Senteces = new Queue<string>();
        SkipB.onClick.AddListener(Skip);
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


        foreach (string sentence in dialogue.sentences)
        {
            Senteces.Enqueue(sentence);
        }
        
    }

    public void Skip()
    {
        // this will give the player the option to skip the dialog



        //animation change (this stuff will mostrpobably be put in a function later on)
        Anim.SetBool("DialogStart", false);
        new WaitForSeconds(1);
        PlayerAnim.SetBool("Fader", false);
        NPCAnim.SetBool("Fader", false);
        
        

        // give back the players movement
        //something needs to be put in place


        // set the camera back to normal
        FindObjectOfType<Camera>().GetComponent<PostProcessingBehaviour>().profile = Default;

        // add the function to make mad man angry
    }

  
}
