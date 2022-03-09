using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventoryUI : MonoBehaviour
{
    inventory inven;

    public inventorySlot[] slots;
    public EquipSlot[] equips;
    public Transform slotHolder;
    public Transform equipHolder;
    void Start()
    {
        inven = inventory.instance;
        slots = slotHolder.GetComponentsInChildren<inventorySlot>();
        equips = equipHolder.GetComponentsInChildren<EquipSlot>();
        inven.onChangeItem += RedrawSlotUI;
        inven.onChangeEquip += RedrawEquipUI;
        gameObject.SetActive(false);

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotnum = i;
        }
        for (int i = 0; i < equips.Length; i++)
        {
            equips[i].slotnum = i;
        }
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
    void RedrawEquipUI()
    {
        for (int i = 0; i < equips.Length; i++)
        {
            equips[i].RemoveSlot();
        }
        for (int i = 0; i < inven.equip.Count; i++)
        {
            equips[i].item = inven.equip[i];
            equips[i].UpdateSlotUI();
        }
    }
}
