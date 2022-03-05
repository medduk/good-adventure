using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryUI : MonoBehaviour
{
    inventory inven;

    public inventorySlot[] slots;
    public Transform slotHolder;
    void Start()
    {
        inven = inventory.instance;
        slots = slotHolder.GetComponentsInChildren<inventorySlot>();
        inven.onChangeItem += RedrawSlotUI;
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RedrawSlotUI()
    {
        for(int i=0; i<slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for(int i=0; i<inven.items.Count; i++)
        {
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }
}
