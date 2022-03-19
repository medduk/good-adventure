using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ReinForceResultslot : MonoBehaviour, IPointerUpHandler
{
    ReinForce RF;
    public Item item;
    public Image icon;
    public Text Text;
    // Start is called before the first frame update
    public void Start()
    {
        RF = ReinForce.instance;
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

    public void OnPointerUp(PointerEventData eventData)
    {
        return;
    }
}