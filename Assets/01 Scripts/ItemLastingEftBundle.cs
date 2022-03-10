using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Eft/Lasting")]
public class ItemLastingEftBundle : ItemEffect
{

    public override bool ExecuteRole(Item _Item)
    {
        if (GameManager.Instance.statusImage.activeSelf == false)
        {

            if (_Item.itemID == 10001)
            {
                PlayerStatus.Instance.RecoveryHp(5);
                return false;
            }
            return true;
        }
        return true;
    }
}

