using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item/Eft/Consumable")]
public class ItemCunsumablesEftBundle : ItemEffect
{

    public override bool ExecuteRole(Item _Item)
    {
        if (_Item.itemID == 10002)
        {
            PlayerStatus.Instance.RecoveryHp(30);
            
        }
        if (_Item.itemID == 10003)
        {
            PlayerStatus.Instance.PlayerDamage += 10;

        }
        return true;
    }
}
