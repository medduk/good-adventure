using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item/Eft/Consumable")]
public class ItemCunsumablesEftBundle : ItemEffect
{

    public override bool ExecuteRole(int ItemID)
    {
        if (ItemID == 10002)
        {
            PlayerStatus.Instance.RecoveryHp(30);
            
        }
        if (ItemID == 10003)
        {
            PlayerStatus.Instance.PlayerDamage += 10;

        }
        return true;
    }
}
