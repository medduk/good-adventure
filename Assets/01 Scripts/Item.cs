using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equip,
    Consumables,
    Etc
}
[System.Serializable]
public class Item
{
    public int itemID;
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public ItemType itemType;


    public bool Use()
    {
        return false;
    }
}
