using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject SoundPrefab;

    public void PlaySound(AudioClip Sound)
    {
        if (Sound)
        {
            GameObject SFX = Instantiate(SoundPrefab);
            AudioSource Clip = SFX.GetComponent<AudioSource>();
            Clip.clip = Sound;
            Clip.Play();

            Destroy(SFX, Clip.clip.length);
        }
    }
}
