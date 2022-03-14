using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Eft/Equip")]
public class ItemEquipEftBundle : ItemEffect
{
    public override bool ExecuteRole(Item _Item)
    {
        if (_Item.itemID == 11001)
        {
            if(_Item.canUse)
                PlayerStatus.Instance.PlayerDamage += 2;
            return false;
        }
        if (_Item.itemID == 11002)
        {
            if (_Item.canUse)
                PlayerStatus.Instance.PlayerDamage += 5;
            return false;
        }
        if (_Item.itemID == 11003)
        {
            if (_Item.canUse)
                PlayerStatus.Instance.PlayerDamage += 10;
            return false;
        }
        if (_Item.itemID == 11004)
        {
            if (_Item.canUse)
                PlayerStatus.Instance.PlayerDamage += 20;
            return false;
        }
        if (_Item.itemID == 11005)
        {
            if (_Item.canUse)
            {
                PlayerStatus.Instance.PlayerDamage += 40;
                PlayerStatus.Instance.PlayerAttackDelay -= 0.2f;
            }
            return false;
        }
        return true;
    }
}