using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeTrap : ToggleScript
{
    private Material Mat;

    private Collider2D Col;



    // Use this for initialization
    protected override void Start()
    {
        Mat = GetComponent<MeshRenderer>().materials[0];
        Col = GetComponent<Collider2D>();

        base.Start();
    }
    


    protected override void OnToggle()
    {
        Mat.color = new Color(1, 1, 1, (Active) ? 1.0f : 0.0f);
        Col.enabled = Active;
    }
}
