using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionScript : MonoBehaviour
{
    public Animation FadeToBlack;


	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(gameObject);
        Debug.Log(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}


    public void GoToScene(string LevelName)
    {
        if (LevelName != "")
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; ++i)
            {
                string Scene = SceneUtility.GetScenePathByBuildIndex(i);
                int LastSlash = Scene.LastIndexOf("/");

                if (LevelName == Scene.Substring(LastSlash + 1, Scene.LastIndexOf(".") - LastSlash - 1))
                {
                    FadeToBlack.Play();
                    StartCoroutine(TransitionTimer(LevelName));
                }
            }
        }

    }


    private IEnumerator TransitionTimer(string LevelName)
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(LevelName);
    }


    public void ReloadScene()
    {
        GoToScene(SceneManager.GetActiveScene().name);
    }
}
