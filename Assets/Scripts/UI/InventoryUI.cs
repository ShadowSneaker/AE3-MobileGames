using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    // this will need to be modified to add multiple of the same item //

    public Transform ItemsParent;
    public GameObject InventoryUIPanel;

    Inventory inventory;

    InventorySlot[] Slots;
	
	void Start ()
    {
        inventory = Inventory.Instance;
        inventory.OnItemChangedCallBack += UpdateUI;

        Slots = ItemsParent.GetComponentsInChildren<InventorySlot>();
	}
	

	void Update ()
    {
		
	}


    void UpdateUI()
    {
        for(int i = 0; i < Slots.Length; i++)
        {
            if(i < inventory.Items.Count)
            {
                Slots[i].AddItem(inventory.Items[i]);
            }
            else
            {
                Slots[i].ClearSlot();
            }
        }
    }

    public void Close_OpenUI()
    {
        InventoryUIPanel.SetActive(!InventoryUIPanel.activeSelf);
    }

}
