using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    ItemScript item;
    public Image Icon;
    public Button RemoveButton;
    public Image ItemInfo;
    private Text[] InfoText;

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
            Debug.Log("NotNull");

            // this part needs finishing later
            item.Use(GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>());

            if(item.DisplayInfo)
            {
                Debug.Log("Entered Display if");
                ItemInfo.enabled = true;
                // enable the display pannel and change the info
                InfoText = ItemInfo.GetComponentsInChildren<Text>();

                //item info 1 is text
                InfoText[0].text = "Name: " + item.ItemName;
                InfoText[0].enabled = true;
                InfoText[1].text = item.Description;
                InfoText[1].enabled = true;
                InfoText[2].text = "Price: " + item.SellValue.ToString();
                InfoText[2].enabled = true;

                ItemInfo.enabled = true;

            }
        }
    }

    public void OnInforClicked()
    {
        InfoText = ItemInfo.GetComponentsInChildren<Text>();

        InfoText[0].enabled = false;
        InfoText[1].enabled = false;
        InfoText[2].enabled = false;
        ItemInfo.enabled = false;
        
    }

}
