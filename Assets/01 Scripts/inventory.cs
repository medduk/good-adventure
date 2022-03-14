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


    public bool[] UseCheck = new bool[6];

    private void Start()
    {
        for(int i = 0; i< UseCheck.Length; i++)
        {
            UseCheck[i] = true;
        }
    }
    private void Update()
    {
        for (int i = 0; i < equip.Count; i++)
        {
            if (UseCheck[i])
            {
                StartCoroutine(UseItem(equip[i], i));
            }
        }
    }

    public bool AddItem(Item _item)
    {
        if (items.Count < 25)
        {
            items.Add(_item);
            if(onChangeItem != null)
            onChangeItem.Invoke();  // ui drawing.
            return true;
        }

        return false;
    }
    public bool EquipItem(Item _item)
    {
        if (equip.Count < 6)
        {
            _item.canUse = _item.Use();
            equip.Add(_item);
            if(onChangeEquip != null)
            onChangeEquip.Invoke(); // ui drawing.
            return true;
        }
        return false;
    }
    public void RemoveItem(int _index)
    {
        items.RemoveAt(_index);
        onChangeItem.Invoke();  // ui drawing.
    }
    public void Unequip(int _index)
    {
        equip.RemoveAt(_index);
        onChangeEquip.Invoke(); // ui drawing.
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

    IEnumerator UseItem(Item _item , int index)
    {
        UseCheck[index] = _item.Use();

        if (GameManager.Instance.statusImage.activeSelf)
        {
            yield return null;
        }

        if (!GameManager.Instance.statusImage.activeSelf)
        {
            yield return new WaitForSeconds(_item.coolTime);
            UseCheck[index] = true;
        }
    }
}
