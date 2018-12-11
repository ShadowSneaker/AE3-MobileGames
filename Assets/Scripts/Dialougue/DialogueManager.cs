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
    //public PostProcessingProfile Blur;
    //public PostProcessingProfile Default;

    // animations for the dialogue box
    public Animator Anim;
    public Animator NextA;
    public Animator EndA;

    //public Button SkipB;
    public Button End;
    public Button Next;

    // creates the postprocessing object to use
    private PostProcessingBehaviour CameraBlur;

    // text for use on the dialog
    public Text CharecterText;
    private string finalline;

    //bool for corutine running
    private bool CR_Running;

    // timer
    private float Timer = 1;

    //bools for timer
    private bool EnableTimer;
    private bool ReverseTimer;

    // player Entity
    Entity playerEntity;

    // MadMan Sprites
    public Sprite EnragedMan;

    // MadMan GameObject
    public GameObject MadMan;

    void Start ()
    {
        CameraBlur = FindObjectOfType<Camera>().GetComponent<PostProcessingBehaviour>();
        Senteces = new Queue<string>();
        MadCount = new Queue<string>();

        //SkipB.onClick.AddListener(Skip);
        Next.onClick.AddListener(ProgressDialogue);
        End.onClick.AddListener(EndDialog);

        CR_Running = false;
        //playerEntity = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
	}

    private void Update()
    {
        if(EnableTimer)
        {
            if(!ReverseTimer)
            {
                if(Timer > 0)
                {
                    Timer -= Time.deltaTime;

                    

                }
                else
                {
                    EnableTimer = false;
                }
            }
            else
            {
                if(Timer < 1)
                {
                    Timer += Time.deltaTime;

                    

                }
                else
                {
                    EnableTimer = false;
                }
            }
            
            DepthOfFieldModel.Settings temp = CameraBlur.profile.depthOfField.settings;
            temp.focusDistance = Timer;
            CameraBlur.profile.depthOfField.settings = temp;

        }
        
        
    }

    public void BeginDialogue(Dialogue dialogue)
    {
        //set animation bool
        Anim.SetBool("DialogStart", true);
        NextA.SetBool("Fader", true);
        // set buttons
        Next.gameObject.SetActive(true);
        //SkipB.gameObject.SetActive(true);

        // still need to set the bools to false in a later function

        // disable player input
        //playerEntity.enabled = false;

        // create a blur on the background
        EnableTimer = true;
        ReverseTimer = false;

        //CameraBlur.profile = Blur;

        // gets all the normal dialog
        foreach (string sentence in dialogue.sentences)
        {
            
            Senteces.Enqueue(sentence);
        }

        // gets all of the angry response sentences
        foreach(string mad in dialogue.Angry)
        {
            MadCount.Enqueue(mad);
        }

        StartCoroutine(TypeWriter(dialogue.StartLine));
        
    }


    public void ProgressDialogue()
    {
        if(CR_Running)
        {
            //clears the text if you skip
            StopAllCoroutines();

            // normal mad text begin
            if (MadCount.Count != 0)
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
                //CameraBlur.profile = Default;

                // give back the players movement
                //playerEntity.enabled = true;
                EnableTimer = true;
                ReverseTimer = true;

                //set the mad mans new sprite
                MadMan.GetComponent<SpriteRenderer>().sprite = EnragedMan;


                MadMan.GetComponent<Entity>().enabled = true;
                
            }
        }
        else
        {
            //part of function for when the dialog ends
            if (Senteces.Count == 0)
            {
                // type out the last sentence
                NextA.SetBool("Fader", false);

                EndA.SetBool("Fader", true);
                StartCoroutine(TypeWriter(finalline));


            }
            else
            {
                //everything else used for normal text
                StartCoroutine(TypeWriter(Senteces.Dequeue()));
            }
        }

        

        
        
    }

    public void EndDialog()
    {
        Anim.SetBool("DialogStart", false);
        new WaitForSeconds(1);

        Next.gameObject.SetActive(false);
        //SkipB.gameObject.SetActive(false);

        //giving the player control again
        //playerEntity.enabled = true;

        // set the camera back to normal
        //CameraBlur.profile = Default;
        EnableTimer = true;
        ReverseTimer = true;

        MadMan.GetComponent<Entity>().enabled = true;
    }

    IEnumerator TypeWriter(string Line)
    {
        CR_Running = true;

        CharecterText.text = "";
        
            foreach (char letter in Line.ToCharArray())
            {
                CharecterText.text += letter;
                yield return null;
            }

        CR_Running = false;
    }
  
    public void GetFinalText(string finaltext)
    {
        finalline = finaltext;
    }
}
