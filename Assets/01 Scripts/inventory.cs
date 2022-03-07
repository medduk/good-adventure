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

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;
    // Start is called before the first frame update
    void Start()
    {
        
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
