using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class inventorySlot : MonoBehaviour, IPointerUpHandler
{
    inventory inven;
    public int slotnum;
    public Item item;
    public Image icon;
    // Start is called before the first frame update

    public void Start()
    {
        inven = inventory.instance;
    }
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

    public void OnPointerUp(PointerEventData eventData)
    {
        bool isEquip = inven.EquipItem(this.item);
        if (isEquip)
        {
            inven.RemoveItem(slotnum);
        }

    }
}
