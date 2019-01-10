using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapTimer : MonoBehaviour {

    public GameObject SpikeTrap1;
    public GameObject SpikeTrap2;

    private float UpTime;
    private float DownTime;


    private bool up;
    private bool down;

	void Start ()
    {
        down = true;
        up = false;

        SpikeTrap1.SetActive(false);
        SpikeTrap2.SetActive(false);

        UpTime = 1.5f;
        DownTime = 3f;
    }
	
	
	void Update ()
    {
		if(down)
        {
            DownTime -= Time.deltaTime;
            if(DownTime <= 0)
            {
                SpikeTrap1.SetActive(true);
                SpikeTrap2.SetActive(true);
                UpTime = 1.5f;
                up = true;
                down = false;
            }
        }
        else if(up)
        {
            UpTime -= Time.deltaTime;
            if(UpTime <= 0)
            {

            }
        }
	}
}
