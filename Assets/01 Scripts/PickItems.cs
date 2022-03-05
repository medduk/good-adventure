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
        item.itemType = _item.itemType;
        item.itemImage = _item.itemImage;

        image.sprite = item.itemImage;
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
