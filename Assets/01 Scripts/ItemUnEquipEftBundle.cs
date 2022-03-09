using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Eft/UnEquip")]
public class ItemUnEquipEftBundle : ItemEffect
{

    public override bool ExecuteRole(Item _Item)
    {
        if (_Item.itemID == 10001)
        {
            PlayerStatus.Instance.PlayerDamage -= 10;
            return true;
        }
        return true;
    }
}
