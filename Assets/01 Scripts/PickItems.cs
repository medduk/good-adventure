using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItems : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;
    
    public void SetItem(Item _item)
    {
        item.itemID = _item.itemID;
        item.itemName = _item.itemName;
        item.itemDescription = _item.itemDescription;
        item.level = _item.level;
        item.coolTime = _item.coolTime;
        item.itemType = _item.itemType;
        item.itemImage = _item.itemImage;
        item.efts = _item.efts;
        item.unefts = _item.unefts;
        image.sprite = item.itemImage;
        item.price = _item.price;
    }

    public Item GetItem()
    {
        return item;
    }
    public void DestoryItem()
    {
        Destroy(gameObject);
    }
}
