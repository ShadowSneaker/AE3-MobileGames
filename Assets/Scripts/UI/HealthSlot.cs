using UnityEngine;
using UnityEngine.UI;

public class HealthSlot : MonoBehaviour
{

    public Image Heart;

    public void AddHeart(Sprite heart)
    {
        Heart.sprite = heart;
    }

    public void RemoveHeart()
    {
        Heart = null;
    }
}
