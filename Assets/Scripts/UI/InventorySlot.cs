using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    ItemScript item;
    public Image Icon;

    public void AddItem(ItemScript newItem)
    {
        item = newItem;
        Icon.sprite = item.Image;
        Icon.enabled = true;
    }

    public void ClearSlot()
    {
        item = null;
        Icon.sprite = null;
        Icon.enabled = false;

    }

}
