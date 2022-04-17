using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Item/Eft/Consumable")]
public class ItemCunsumablesEftBundle : ItemEffect
{
    public override bool ExecuteRole(Item _Item)
    {
        if (_Item.itemID == 10001)
        {
            PlayerStatus.Instance.RecoveryHp(10);
            
        }
        if (_Item.itemID == 10002)
        {
            PlayerStatus.Instance.RecoveryHp(30);

        }
        if (_Item.itemID == 10003)
        {
            PlayerStatus.Instance.RecoveryHp(50);

        }
        if (_Item.itemID == 10950)
        {
            int Count = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), 0));
            PlayerPrefs.SetInt(System.Enum.GetName(typeof(Runes), 0), Count + 1);
            PlayerStatus.Instance.PlayerMaxHp += 2;
            PlayerStatus.Instance.HPText();

        }
        if (_Item.itemID == 10951)
        {
            int Count = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), 1));
            PlayerPrefs.SetInt(System.Enum.GetName(typeof(Runes), 1), Count + 1);
            PlayerStatus.Instance.PlayerDamage += 1;

        }
        if (_Item.itemID == 10952)
        {
            int Count = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), 2));
            PlayerPrefs.SetInt(System.Enum.GetName(typeof(Runes), 2), Count + 1);
            PlayerStatus.Instance.PlayerChangeAttackDelay -= 0.01f;

        }
        if (_Item.itemID == 10953)
        {
            int Count = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), 3));
            PlayerPrefs.SetInt(System.Enum.GetName(typeof(Runes), 3), Count + 1);
            PlayerStatus.Instance.PlayerMoveSpeed += 0.02f;

        }
        if (_Item.itemID == 10954)
        {
            int Count = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), 4));
            PlayerPrefs.SetInt(System.Enum.GetName(typeof(Runes), 4), Count + 1);
            PlayerStatus.Instance.PlayerDefense += 1;

        }
        if (_Item.itemID == 10955)
        {
            int Count = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), 5));
            PlayerPrefs.SetInt(System.Enum.GetName(typeof(Runes), 5), Count + 1);
            PlayerStatus.Instance.CriticalDamage += 0.02f;

        }
        if (_Item.itemID == 10956)
        {
            int Count = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), 6));
            PlayerPrefs.SetInt(System.Enum.GetName(typeof(Runes), 6), Count + 1);
            PlayerStatus.Instance.CriticalProbability += 0.5f;

        }
        if (_Item.itemID == 10957)
        {
            int Count = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), 7));
            PlayerPrefs.SetInt(System.Enum.GetName(typeof(Runes), 7), Count + 1);
            PlayerStatus.Instance.AbsorptionOfVitality += 0.005f;

        }
        return true;
    }
}
