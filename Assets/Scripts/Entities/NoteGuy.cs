using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGuy : MonoBehaviour
{
    public float ShineDuration = 1.0f;
    public Color ShineColour;
    

    private SpriteRenderer Rend;
    private Color DefaultColour;

    private void Start()
    {
        Rend = GetComponent<SpriteRenderer>();
        DefaultColour = Rend.color;
    }


    public void Activate()
    {
        Rend.color = ShineColour;
        StartCoroutine(Duration());
    }


    private IEnumerator Duration()
    {
        yield return new WaitForSeconds(ShineDuration);
        Rend.color = DefaultColour;
    }

}
