using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    // this will need to be modified to add multiple of the same item //

    
    Inventory inventory;

	
	void Start ()
    {
        inventory = Inventory.Instance;
        inventory.OnItemChangedCallBack += UpdateUI;
	}
	

	void Update ()
    {
		
	}


    void UpdateUI()
    {
        Debug.Log("Updating UI");
    }

}
