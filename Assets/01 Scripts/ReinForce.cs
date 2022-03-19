using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ReinForce : MonoBehaviour
{
    public static ReinForce instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    inventory inven;

    public ReinForceInvenslot[] slots;
    public ReinForceslot[] ReinForces;
    public ReinForceResultslot Result;
    public Transform slotHolder;
    public Transform ReinForceHolder;

    public List<Item> items = new List<Item>();
    public List<Item> ReForce = new List<Item>();
    public Item ReinForceResult;
    private void Start()
    {
        inven = inventory.instance;
        slots = slotHolder.GetComponentsInChildren<ReinForceInvenslot>();
        ReinForces = ReinForceHolder.GetComponentsInChildren<ReinForceslot>();

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].slotnum = i;
        }
        for (int i = 0; i < ReinForces.Length; i++)
        {
            ReinForces[i].slotnum = i;
        }

        ReinForceStart();
    }

    public void ReinForceStart()
    {
        
        items = inven.items.ToList();
           
        RedrawRFSlotUI();
        CanRFdraw();
        unCanRFdraw();

    }


    void RedrawRFSlotUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].RemoveSlot();
        }
        for (int i = 0; i < items.Count; i++)
        {
            items[i].canUse = true;
            slots[i].item = items[i];
            slots[i].UpdateSlotUI();
        }
    }
    void RedrawReinForceUI()
    {
        for (int i = 0; i < ReinForces.Length; i++)
        {
            ReinForces[i].RemoveSlot();
        }
        for (int i = 0; i < ReForce.Count; i++)
        {
            ReinForces[i].item = ReForce[i];
            ReinForces[i].UpdateSlotUI();
        }
    }
    void CanRFdraw()
    {
        for (int i = 0; i < items.Count; i++)
        {
            slots[i].canRF();
        }
    }
    void unCanRFdraw()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].level == 5)
            {
                slots[i].uncanRF();
            }
            if(ReForce.Count == 1)
            {
                if(ReForce[0].itemID != items[i].itemID)
                {
                    slots[i].uncanRF();
                }
            }
        }
    }

    public bool Addlist(Item _item)
    {
        if (items.Count < 25)
        {
            items.Add(_item);
            RedrawRFSlotUI();
            return true;
        }

        return false;
    }
    public bool ReinForceItem(Item _item)
    {
        if (_item.level != 5)
        {
            if (ReForce.Count == 0)
            {
                ReForce.Add(_item);
                RedrawReinForceUI();
                return true;
            }
            if (ReForce.Count == 1)
            {
                if (ReForce[0].itemID == _item.itemID)
                {
                    ReForce.Add(_item);
                    RedrawReinForceUI();
                    return true;
                }
            }
            
        }
        return false;
    }
    public void Removelist(int _index)
    {
        items.RemoveAt(_index);
        RedrawRFSlotUI();
        CanRFdraw();
        unCanRFdraw();
    }
    
    public void RemoveReinForceItem(int _index)
    {
        ReForce.RemoveAt(_index);
        RedrawReinForceUI();
        CanRFdraw();
        unCanRFdraw();
        
    }

    private void Update()
    {
        if (ReForce.Count == 2) 
        {
            if (ReForce[0].itemID == ReForce[1].itemID && Result.item == null)
            {
                Debug.Log("성공!");
                ReinForceResult = ItemBundle.instance.ReinForce(ReForce[0].itemID + 1);
                Result.item = ReinForceResult;
                Result.UpdateSlotUI();
            }
        }

        if (Result.item != null && ReForce.Count < 2)
        {
            Debug.Log("초기화!");
            ReinForceResult = null;
            Result.RemoveSlot();
        }
    }

    public void ReinForceSuccess()
    {
        
        if(ReForce.Count == 2 && Result.item != null)
        {
            Addlist(ReinForceResult);
            RemoveReinForceItem(0);
            RemoveReinForceItem(0);
        }
    }
}
