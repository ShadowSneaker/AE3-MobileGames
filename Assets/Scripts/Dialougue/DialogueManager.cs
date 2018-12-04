using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    // creates a que of sentences that the npc can iterate through
    private Queue<string> Senteces;
    // text that each charecter will play once all main dialogue is complete
    private string LoopText;
    // the background blur variables
    public PostProcessingProfile Blur;
    public PostProcessingProfile Default;

    // animations for the dialogue box
    public Animator Anim;
    public Animator PlayerAnim;
    public Animator NPCAnim;

    public Button SkipB;

    // creates the postprocessing object to use
    private PostProcessingBehaviour CameraBlur;
    // text for use on the dialog
    public Text CharecterText;

	void Start ()
    {
        CameraBlur = FindObjectOfType<Camera>().GetComponent<PostProcessingBehaviour>();
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
        CameraBlur.profile = Blur;


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
        CameraBlur.profile = Default;

        // add the function to make mad man angry and begin to attack


        //DepthOfFieldModel.Settings temp = new DepthOfFieldModel.Settings();
        //temp.aperture = Mathf.Lerp(0.01f, 5.5f, Time.time);
        //FindObjectOfType<Camera>().GetComponent<PostProcessingBehaviour>().profile.depthOfField.settings = temp; 
    }

    public void ProgressDialogue()
    {
        //part of function for when the dialog ends
        if(Senteces.Count == 0)
        {

        }
        //everything else used for normal text
    }

    IEnumerator TypeWriter(string Line)
    {
        CharecterText.text = "";
        foreach(char letter in Line.ToCharArray())
        {
            CharecterText.text += letter;
            yield return null;
        }
    }
  
}
