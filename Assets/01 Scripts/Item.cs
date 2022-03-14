using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equip,
    Lasting,
    Consumables,
    Etc
}
[System.Serializable]
public class Item
{
    public int itemID;

    public bool canUse = true;
    public string itemName;
    public string itemDescription;
    public int level = 1;
    public float coolTime;
    public Sprite itemImage;
    public ItemType itemType;
    public List<ItemEffect> efts;
    public List<ItemEffect> unefts;

    public bool Use()
    {
        bool isUsed = false;
        foreach (ItemEffect eft in efts)
        {
            isUsed = isUsed & eft.ExecuteRole(this);
        }
        return isUsed;
    }
    public bool UnUse()
    {
        bool isUsed = false;
        foreach (ItemEffect eft in unefts)
        {
            isUsed = isUsed & eft.ExecuteRole(this);
        }
        return isUsed;
    }
}
