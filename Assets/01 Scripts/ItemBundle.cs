using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBundle : MonoBehaviour
{
    public static ItemBundle instance;

    private void Awake()
    {
        instance = this;
    }
    public List<Item> itemBundle = new List<Item>();

    public GameObject pickItemPrefab;
    public Vector3[] pos;
    private void Start()
    {
        for(int i = 0; i<4; i++)
        {
            GameObject go = Instantiate(pickItemPrefab, pos[i], Quaternion.identity);
            go.GetComponent<PickItems>().SetItem(itemBundle[Random.Range(0,9)]);
        }
    }

    public void Drop(Vector3 i , int indexkey)
    {
        if (indexkey == 0)
            return;

        GameObject go = Instantiate(pickItemPrefab,i, Quaternion.identity);
        go.GetComponent<PickItems>().SetItem(itemBundle[itemBundle.FindIndex(a => a.itemID ==indexkey)]);
    }

    public Item makeItem(int indexkey)
    {
        return itemBundle[itemBundle.FindIndex(a => a.itemID == indexkey)];
    }
}
