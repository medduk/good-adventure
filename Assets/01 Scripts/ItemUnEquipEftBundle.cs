using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Eft/UnEquip")]
public class ItemUnEquipEftBundle : ItemEffect
{
    public override bool ExecuteRole(Item _Item)
    {
        if (_Item.itemID == 11001)
        {
            if (_Item.canUse)
            {
                PlayerStatus.Instance.PlayerDamage -= 2;
                return true;
            }
        }
        if (_Item.itemID == 11002)
        {
            if (_Item.canUse)
            {
                PlayerStatus.Instance.PlayerDamage -= 5;
                return true;
            }
        }
        if (_Item.itemID == 11003)
        {
            if (_Item.canUse)
            {
                PlayerStatus.Instance.PlayerDamage -= 10;
                return true;
            }
        }
        if (_Item.itemID == 11004)
        {
            if (_Item.canUse)
            {
                PlayerStatus.Instance.PlayerDamage -= 20;
                return true;
            }
        }
        if (_Item.itemID == 11005)
        {
            if (_Item.canUse)
            {
                PlayerStatus.Instance.PlayerDamage -= 40;
                PlayerStatus.Instance.PlayerAttackDelay += 0.2f;

                return true;
            }
        }
        if (_Item.itemID == 20001)
        {
            if (_Item.canUse)
            {
                PlayerMagicManager.Instance.playerMagics[0].SetActive(false);
                return true;
            }
        }
        return false;
    }
}
