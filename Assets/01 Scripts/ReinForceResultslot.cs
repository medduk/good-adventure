using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ReinForceResultslot : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    ReinForce RF;
    public Item item;
    public Image icon;
    public Text Text;
    private float timer;
    private bool click;
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
    private void Update()
    {
        if (click)
        {
            timer += Time.deltaTime;
        }
        if (click && timer > 0.5f)
        {
            click = false;
            ItemInformation.Instance.item = this.item;
            ItemInformation.Instance.Show();
            GameManager.Instance.OpenItemInformation(false);

        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (this.item != null)
        {
            click = false;
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
