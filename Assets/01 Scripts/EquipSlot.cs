using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class EquipSlot : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{

    inventory inven;
    public int slotnum;
    public Item item;
    public Image icon;
    public Text Text;
    private bool click;
    // Start is called before the first frame update
    public void Start()
    {
        inven = inventory.instance;
    }
    public void UpdateSlotUI()
    {
        icon.sprite = item.itemImage;
        icon.gameObject.SetActive(true);
        Text.gameObject.SetActive(false);
    }

    public void RemoveSlot()
    {
        item = null;
        icon.gameObject.SetActive(false);
        Text.gameObject.SetActive(true);
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
            GameManager.Instance.OpenItemInformation(false);
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
                
                bool isEquip = inven.AddItem(this.item);
                
                if (isEquip)
                {
                    this.item.UnUse();
                    inven.Unequip(slotnum);
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
