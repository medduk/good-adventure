using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ReinForce : MonoBehaviour  // Inventroy ��ũ��Ʈ�� �����ϰ� ���� �κ��丮�� ���¸� �ҷ��ͼ� ��ȭUI�� ��ȭ���� �������� �۾��� �κ��丮�� �����ϴ� ���
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

        gameObject.SetActive(false);
    }

    public void ReinForceStart()
    {
        
        this.items = inven.items.ToList();
           
        RedrawRFSlotUI();
        CanRFdraw();
        unCanRFdraw();

    }
    public void ReinForceEnd()
    {
        int j = ReForce.Count;
        for (int i = 0; i < j ; i++)
        {
            Addlist(ReForce[0]);
            RemoveReinForceItem(0);
        }
        inven.items = null;
        inven.items = this.items.ToList();
        inven.onChangeItem();
        this.items = null;
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
    void CanRFdraw() // �⺻������ ��ϰ����ϰ� �������ϰ� ǥ������ ����
    {
        for (int i = 0; i < items.Count; i++)
        {
            slots[i].canRF();
        }
    }
    void unCanRFdraw() // �������� �������ϰ� ǥ���Ͽ� ��ϺҰ��� ǥ��
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].level >= 5)
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
    public bool ReinForceItem(Item _item)  // �ְ����� �ƴ� �������� ���ʵ���� ���� �����ۿܿ��� ��ϺҰ��� ǥ����
    {
        if (_item.level < 5)
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

    private void Update() // ���� ������ �ΰ��� ������������ ����� ���
    {
        if (ReForce.Count == 2) 
        {
            if (ReForce[0].itemID == ReForce[1].itemID && Result.item == null)
            {
                Debug.Log("����!");
                ReinForceResult = ItemBundle.instance.makeItem(ReForce[0].itemID + 1);
                Result.item = ReinForceResult;
                Result.UpdateSlotUI();
            }
        }

        if (Result.item != null && ReForce.Count < 2)
        {
            Debug.Log("�ʱ�ȭ!");
            ReinForceResult = null;
            Result.RemoveSlot();
        }
    }

    public void ReinForceSuccess() // ��ȭ����� ����������
    {
        
        if(ReForce.Count == 2 && Result.item != null)
        {
            Addlist(ReinForceResult);
            RemoveReinForceItem(0);
            RemoveReinForceItem(0);
        }
    }
}
