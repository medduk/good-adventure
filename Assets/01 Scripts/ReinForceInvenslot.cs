using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ReinForceInvenslot : MonoBehaviour, IPointerUpHandler
{
    ReinForce RF;
    public int slotnum;
    public Item item;
    public Image icon;
    // Start is called before the first frame update

    public void Start()
    {
        RF = ReinForce.instance;
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
        if(this.item != null)
        {
            bool isReinForce = RF.ReinForceItem(this.item);
            if (isReinForce)
            {
                RF.Removelist(slotnum);
            }
        }
    }
}