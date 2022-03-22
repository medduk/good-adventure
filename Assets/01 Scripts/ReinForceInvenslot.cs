using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ReinForceInvenslot : MonoBehaviour, IPointerUpHandler , IPointerDownHandler
{
    ReinForce RF;
    public int slotnum;
    public Item item;
    public Image icon;
    Color color = new Color (1f,1f,1f);
    public bool canReinForce;
    private float timer;
    private bool click;
    // Start is called before the first frame update

    public void Start()
    {
        RF = ReinForce.instance;
        canReinForce = true;
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

    public void canRF()
    {
        if (!canReinForce || item.level !=5) 
        { 
        canReinForce = true;
            color.a = 1f;
            icon.color = color;
        }
    }
    public void uncanRF()
    {
        if (canReinForce)
        {
            color.a = 0.25f;
            icon.color = color;
        }
    }
    private void Update()
    {
        if (click)
        {
            timer += Time.deltaTime;
        }
        if (click && timer > 0.5f)
        {
            click = false;
            Debug.Log("길게 눌러써요 ! ");
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (this.item != null)
        {
            click = false;
            if (timer < 0.5f)
            {
                timer = 0f;
                bool isReinForce = RF.ReinForceItem(this.item);
                if (isReinForce)
                {
                    RF.Removelist(slotnum);
                }
            }
            timer = 0f;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (this.item != null)
        {
            click = true;
        }
    }
}
