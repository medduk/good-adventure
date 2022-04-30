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
            
                PlayerStatus.Instance.PlayerDamage -= 2;
                return true;
            
        }
        if (_Item.itemID == 11002)
        {
            
                PlayerStatus.Instance.PlayerDamage -= 5;
                return true;
            
        }
        if (_Item.itemID == 11003)
        {
            
                PlayerStatus.Instance.PlayerDamage -= 10;
                return true;
            
        }
        if (_Item.itemID == 11004)
        {
            
                PlayerStatus.Instance.PlayerDamage -= 20;
                return true;
            
        }
        if (_Item.itemID == 11005)
        {
            
                PlayerStatus.Instance.PlayerDamage -= 40;
                PlayerStatus.Instance.PlayerChangeAttackDelay += 0.2f;

                return true;
            
        }
        if (_Item.itemID == 20001)
        {

                PlayerMagicManager.Instance.playerMagics[0].SetActive(false);
                return true;
            
        }
        return false;
    }
}
