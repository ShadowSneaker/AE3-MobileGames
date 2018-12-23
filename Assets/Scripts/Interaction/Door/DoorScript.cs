﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorScript : ActivatableObject
{
    public string GoToRoom;

    public ItemScript UnlockItem;

    private Animator Anim;


    // Use this for initialization
    void Start()
    {
        Anim = GetComponent<Animator>();

        Anim.SetBool("Open", Activated);
    }

    private void Update()
    {
        Anim.SetBool("Open", Activated);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only go to the next scene if the player goes through the door.
        if (collision.gameObject.CompareTag("Player"))
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
        
    }


    public override void OnActivate()
    {
        base.Activate();

        Anim.SetBool("Open", Activated);
    }
}
