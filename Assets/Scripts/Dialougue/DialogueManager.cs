using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //stuff for depth of field thing
    //DepthOfFieldModel.Settings temp = new DepthOfFieldModel.Settings();
    //temp.aperture = Mathf.Lerp(0.01f, 5.5f, Time.time);
    //FindObjectOfType<Camera>().GetComponent<PostProcessingBehaviour>().profile.depthOfField.settings = temp; 


    // creates a que of sentences that the npc can iterate through
    private Queue<string> Senteces;
    private Queue<string> MadCount;

    // text that each charecter will play once all main dialogue is complete
    private string LoopText;
    // the background blur variables
    public PostProcessingProfile Blur;
    public PostProcessingProfile Default;

    // animations for the dialogue box
    public Animator Anim;
    public Animator NextA;
    public Animator EndA;

    public Button SkipB;
    public Button End;
    public Button Next;


    // creates the postprocessing object to use
    private PostProcessingBehaviour CameraBlur;

    // text for use on the dialog
    public Text CharecterText;
    private string finalline;

    private string DisplaySentence;

    // change the dialog for madman to get angry

    void Start ()
    {
        CameraBlur = FindObjectOfType<Camera>().GetComponent<PostProcessingBehaviour>();
        Senteces = new Queue<string>();
        MadCount = new Queue<string>();

        SkipB.onClick.AddListener(Skip);
        Next.onClick.AddListener(ProgressDialogue);
        End.onClick.AddListener(EndDialog);
	}


    public void BeginDialogue(Dialogue dialogue)
    {
        //set animation bool
        Anim.SetBool("DialogStart", true);
        NextA.SetBool("Fader", true);

        // still need to set the bools to false in a later function

        // disable player input
        // something needs to be put in place

        // create a blur on the background
        CameraBlur.profile = Blur;

        // gets all the normal dialog
        foreach (string sentence in dialogue.sentences)
        {
            DisplaySentence = sentence;
            Senteces.Enqueue(sentence);
        }

        // gets all of the angry response sentences
        foreach(string mad in dialogue.Angry)
        {
            MadCount.Enqueue(mad);
        }

        StartCoroutine(TypeWriter(dialogue.StartLine));
        
    }

    public void Skip()
    {
        // this will give the player the option to skip the dialog sentences


        //clears the text if you skip
        StopAllCoroutines();
        
        // normal mad text begin
        if(MadCount.Count != 0)
        {
            
            CharecterText.text = "";
            StartCoroutine(TypeWriter(MadCount.Dequeue()));
        }
        

        if (MadCount.Count == 0)
        {
            // add the entity to the madman and begin fight

            //animation change (this stuff will mostrpobably be put in a function later on)
            Anim.SetBool("DialogStart", false);
            new WaitForSeconds(1);

            // set the camera back to normal
            CameraBlur.profile = Default;

            // give back the players movement
            //something needs to be put in place
        }


    }

    public void ProgressDialogue()
    {
        //clears the text if you skip
        StopAllCoroutines();

        //part of function for when the dialog ends
        if (Senteces.Count == 0)
        {
            // type out the last sentence
            NextA.SetBool("Fader", false);
            EndA.SetBool("Fader", true);
            StartCoroutine(TypeWriter(finalline));
            // have normal next button and skip disaper and end button appear


        }
        else
        {
            //everything else used for normal text
            StartCoroutine(TypeWriter(Senteces.Dequeue()));
        }
        
    }

    public void EndDialog()
    {
        Anim.SetBool("DialogStart", false);
        new WaitForSeconds(1);

        // set the camera back to normal
        CameraBlur.profile = Default;
    }

    IEnumerator TypeWriter(string Line)
    {

        CharecterText.text = "";
        
            foreach (char letter in Line.ToCharArray())
            {
                CharecterText.text += letter;
                yield return null;
            }

        
    }
  
    public void GetFinalText(string finaltext)
    {
        finalline = finaltext;
    }
}
