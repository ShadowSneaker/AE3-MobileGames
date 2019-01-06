using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPotion", menuName = "Inventory/Potion")]
public class Potion : ItemScript
{
    public enum PotionType
    {
        Health,
        Poison
    }


    // The type of effect this potion will have.
    public PotionType Type;

    // How powerful this potion is.
    // For example if this is a health potion it will heal this amount of points.
    public int PotionStrength;



    public override void Use(Entity User)
    {
        switch (Type)
        {
            case PotionType.Health:
                User.ApplyHeal(PotionStrength);
                break;


            case PotionType.Poison:
                User.ApplyDamage(PotionStrength, null);
                break;
        }
    }
}
