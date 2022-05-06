using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class inventorySlot : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    inventory inven;
    public int slotnum;
    public Item item;
    public Image icon;
    private bool click;

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
    IEnumerator openInformation()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        if (click)
        {
            click = false;
            ItemInformation.Instance.item = this.item;
            ItemInformation.Instance.slotnum = this.slotnum;
            ItemInformation.Instance.Show();
            GameManager.Instance.OpenItemInformation(true);
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (this.item != null)
        {
            if (click)
            {
                StopCoroutine("openInformation");
                click = false;
                bool isEquip = inven.EquipItem(this.item);
                if (isEquip)
                {
                    inven.RemoveItem(slotnum);
                }
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (this.item != null)
        {
            click = true;
            StartCoroutine("openInformation");
        }
    }
}
