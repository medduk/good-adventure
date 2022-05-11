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
        RedrawEquipUI();
        RedrawSlotUI();
        //gameObject.SetActive(false);

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotnum = i;
        }
        for (int i = 0; i < equips.Length; i++)
        {
            equips[i].slotnum = i;
        }
        gameObject.SetActive(false);
    }

    void RedrawSlotUI()  // 아이템에 따라UI 그리기 , 리스트를 통하여 아이템을 관리하기 때문에 전부 지웠다가 다시 그리는 방식으로 채택
    {
        for(int i=0; i<slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for(int i=0; i<inven.items.Count; i++)
        {
            inven.items[i].canUse = true;
            slots[i].item = inven.items[i];
            slots[i].UpdateSlotUI();
        }
    }
    void RedrawEquipUI() // 아이템에 따라UI 그리기
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
