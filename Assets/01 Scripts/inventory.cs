using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory : MonoBehaviour
{

    public static inventory instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public List<Item> items = new List<Item>();
    public List<Item> equip = new List<Item>();
    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public delegate void OnChangeEquip();
    public OnChangeEquip onChangeEquip;
    // Start is called before the first frame update
    private void Update()
    {
        for (int i = 0; i < equip.Count; i++)
        {
            equip[i].canUse = equip[i].Use();
        }
    }

    // Update is called once per frame
    public bool AddItem(Item _item)
    {
        if (items.Count < 25)
        {
            items.Add(_item);
            if(onChangeItem != null)
            onChangeItem.Invoke();
            return true;
        }
        return false;
    }
    public bool EquipItem(Item _item)
    {
        if (equip.Count < 6)
        {
            equip.Add(_item);
            if(onChangeEquip != null)
            onChangeEquip.Invoke();
            return true;
        }
        return false;
    }
    public void RemoveItem(int _index)
    {
        items.RemoveAt(_index);
        onChangeItem.Invoke();
    }
    public void ReEquip(int _index)
    {
        equip.RemoveAt(_index);
        onChangeEquip.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickItem"))
        {
            PickItems pickItems = collision.GetComponent<PickItems>();
            if (pickItems.item.itemType == ItemType.Consumables)
            {
                pickItems.item.Use();
                pickItems.DestoryItem();
                return;
            }
            if (AddItem(pickItems.GetItem()))
                pickItems.DestoryItem();
        }
    }
}
