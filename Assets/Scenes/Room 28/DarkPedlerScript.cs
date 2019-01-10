using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DarkPedlerScript : MonoBehaviour {

    public GameObject SellPage;
    public GameObject BuyPage;

    public GameObject Torch;

    // add a button to exit
    //public Button Exit;

    //public Button BuyTorch;

    public int tokens;

    private Inventory PlayerInventory;

    void Start ()
    {
        SellPage.SetActive(false);
        BuyPage.SetActive(false);

        Torch.SetActive(false);

        PlayerInventory = FindObjectOfType<Inventory>();
    }
	
	
	void Update ()
    {
		
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        SellPage.SetActive(true);
        BuyPage.SetActive(true);
    }

    public void SellBoneDust()
    {
        for (int i = 0; i <  PlayerInventory.Items.Count; i++ )
        {
            if(PlayerInventory.Items[i].ItemName == "Bonedust")
            {
                tokens += 2;
            }
        }
    }

    public void SellDummyBolt()
    {
        for (int i = 0; i < PlayerInventory.Items.Count; i++)
        {
            if (PlayerInventory.Items[i].ItemName == "DummyBolt")
            {
                tokens += 2;
            }
        }
    }

    public void SellSpectralDust()
    {
        for (int i = 0; i < PlayerInventory.Items.Count; i++)
        {
            if (PlayerInventory.Items[i].ItemName == "Spectral Dust")
            {
                tokens += 2;
            }
        }
    }

    public void SellSkull()
    {
        for (int i = 0; i < PlayerInventory.Items.Count; i++)
        {
            if (PlayerInventory.Items[i].ItemName == "Skull")
            {
                tokens += 2;
            }
        }
    }

    public void SellGoblinPie()
    {
        for (int i = 0; i < PlayerInventory.Items.Count; i++)
        {
            if (PlayerInventory.Items[i].ItemName == "Goblin Pie")
            {
                tokens += 2;
            }
        }
    }

    public void SellEssenceEarth()
    {
        for (int i = 0; i < PlayerInventory.Items.Count; i++)
        {
            if (PlayerInventory.Items[i].ItemName == "Earth essence")
            {
                tokens += 2;
            }
        }
    }

    public void SellessenceAir()
    {
        for (int i = 0; i < PlayerInventory.Items.Count; i++)
        {
            if (PlayerInventory.Items[i].ItemName == "Air Essence")
            {
                tokens += 2;
            }
        }
    }

    public void SellEssenceUndeath()
    {
        for (int i = 0; i < PlayerInventory.Items.Count; i++)
        {
            if (PlayerInventory.Items[i].ItemName == "Undead essence")
            {
                tokens += 2;
            }
        }
    }

    public void SellEssenceFire()
    {
        for (int i = 0; i < PlayerInventory.Items.Count; i++)
        {
            if (PlayerInventory.Items[i].ItemName == "fire essence")
            {
                tokens += 2;
            }
        }
    }

    public void SellEssenceVolcanic()
    {
        for (int i = 0; i < PlayerInventory.Items.Count; i++)
        {
            if (PlayerInventory.Items[i].ItemName == "Volcanic Essence")
            {
                tokens += 2;
            }
        }
    }

    public void SellMushroomHead()
    {
        for (int i = 0; i < PlayerInventory.Items.Count; i++)
        {
            if (PlayerInventory.Items[i].ItemName == "Mushroom Head")
            {
                tokens += 2;
            }
        }
    }


    public void BuyTorch()
    {
        if(tokens == 10)
        {
            Torch.SetActive(true);

            SellPage.SetActive(false);
            BuyPage.SetActive(false);
        }
    }

    public void ExitButton()
    {
        SellPage.SetActive(false);
        BuyPage.SetActive(false);
    }

}
