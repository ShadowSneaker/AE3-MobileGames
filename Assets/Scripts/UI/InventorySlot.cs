using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    ItemScript item;
    public Image Icon;
    public Button RemoveButton;

    public void AddItem(ItemScript newItem)
    {
        item = newItem;
        Icon.sprite = item.Image;
        Icon.enabled = true;
        RemoveButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        Icon.sprite = null;
        Icon.enabled = false;
        RemoveButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.Instance.RemoveItems(item);
    }

    public void UseItem()
    {
        if(item != null)
        {
            // this part needs finishing later
            item.Use();
        }
    }

}
