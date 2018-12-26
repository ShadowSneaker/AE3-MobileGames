using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public bool Play;

    public Transform CameraTransform;

    public float ShakeDuration = 0.2f;

    public float ShakeAmount = 0.7f;
    public float DecreaseFactor = 1.0f;

    Vector3 OrigionalPos;
  


    private void OnEnable()
    {
        if (CameraTransform)
        {
            OrigionalPos = CameraTransform.localPosition;
        }
    }

    // Update is called once per frame
    void Update ()
    {
		if (CameraTransform && Play)
        {
            if (ShakeDuration > 0)
            {
                OrigionalPos = CameraTransform.localPosition;
                Vector2 Shake = Random.insideUnitCircle;
                CameraTransform.localPosition = OrigionalPos + new Vector3(Shake.x, Shake.y, 0.0f) * ShakeAmount;
                ShakeDuration -= DecreaseFactor * Time.deltaTime;
            }
            else
            {
                ShakeDuration = 0.0f;
                CameraTransform.localPosition = OrigionalPos;
                Play = false;
            }
        }
	}
}
