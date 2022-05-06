using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBundle : MonoBehaviour
{
    public Transform dataManager;

    public static ItemBundle instance;

    private void Awake()
    {
        instance = this;
    }
    public List<Item> itemBundle = new List<Item>();

    public GameObject pickItemPrefab;
    public GameObject ItemBox;

    public void Drop(Vector3 i , int indexkey)
    {
        if (indexkey == 0)
            return;

        if (indexkey == 1)
        {
            GameObject goo = Instantiate(ItemBox, i, Quaternion.identity);
            goo.transform.SetParent(dataManager);
            return;
        }

        GameObject go = Instantiate(pickItemPrefab,i, Quaternion.identity);
        go.transform.SetParent(dataManager);
        go.GetComponent<PickItems>().SetItem(itemBundle[itemBundle.FindIndex(a => a.itemID == indexkey)]);
    }

    public Item MakeItem(int indexkey)
    {
        GameObject go = Instantiate(pickItemPrefab, new Vector3(1000,1000), Quaternion.identity);
        go.GetComponent<PickItems>().SetItem(itemBundle[itemBundle.FindIndex(a => a.itemID == indexkey)]);
        Item a = go.GetComponent<PickItems>().GetItem();
        Destroy(go);
        return a;
    }   
}
