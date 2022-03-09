using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Eft/Equip")]
public class ItemEquipEftBundle : ItemEffect
{

    public override bool ExecuteRole(Item _Item)
    {
        if (_Item.itemID == 10001)
        {
            if(_Item.canUse)
            PlayerStatus.Instance.PlayerDamage += 10;
            return false;
        }
        return true;
    }
}