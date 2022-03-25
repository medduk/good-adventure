using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Item/Eft/Lasting")]
public class ItemLastingEftBundle : ItemEffect
{
    public override bool ExecuteRole(Item _Item)
    {
        if (!GameManager.Instance.statusImage.activeSelf)
        {
            if (_Item.itemID == 20001)
            {
                GameManager.Instance.StartCoroutine(Id20001());
                return false;
            }
        
        }
        return true;
    }
    IEnumerator Id20001() 
    {
        PlayerStatus.Instance.PlayerDamage += 30;
        yield return new WaitForSeconds(10f);
        PlayerStatus.Instance.PlayerDamage -= 30;
    }
}

