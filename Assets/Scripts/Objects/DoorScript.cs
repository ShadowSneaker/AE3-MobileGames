using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Replace MonoBehaviour with the Activation Script.
public class DoorScript : ActivatableObject
{
    public string GoToRoom;

    private Animator Anim;

    // Use this for initialization
    void Start()
    {
        Anim = GetComponent<Animator>();

        Anim.SetBool("Open", Activated);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GoToRoom != "")
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; ++i)
            {
                string Scene = SceneUtility.GetScenePathByBuildIndex(i);
                int LastSlash = Scene.LastIndexOf("/");

                if (GoToRoom == Scene.Substring(LastSlash + 1, Scene.LastIndexOf(".") - LastSlash - 1))
                {
                    SceneManager.LoadScene(GoToRoom);
                }
            }
        }
    }


    public override void Activate()
    {
        base.Activate();

        Anim.SetBool("Open", Activated);
    }
}
