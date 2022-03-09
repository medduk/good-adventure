using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class EquipSlot : MonoBehaviour, IPointerUpHandler
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

    public void OnPointerUp(PointerEventData eventData)
    {

    }
}
