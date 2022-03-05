using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class inventorySlot : MonoBehaviour
{
    public Item item;
    public Image icon;
    // Start is called before the first frame update

    public void UpdateSlotUI()
    {
        icon.sprite = item.itemImage;
        icon.gameObject.SetActive(true);
    }

    public void RemoveSlot()
    {
        item = null;
        icon.gameObject.SetActive(false);
    }
}
