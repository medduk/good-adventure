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
    public Vector3[] pos;
    private void Start()
    {
        for(int i = 0; i<4; i++)
        {
            GameObject go = Instantiate(pickItemPrefab, pos[i], Quaternion.identity);
            go.GetComponent<PickItems>().SetItem(itemBundle[Random.Range(9,10)]);
            go.transform.SetParent(dataManager);
        }
    }

    public void Drop(Vector3 i , int indexkey)
    {
        if (indexkey == 0)
            return;

        GameObject go = Instantiate(pickItemPrefab,i, Quaternion.identity);
        go.transform.SetParent(dataManager);
        go.GetComponent<PickItems>().SetItem(itemBundle[itemBundle.FindIndex(a => a.itemID == indexkey)]);
    }

    public Item makeItem(int indexkey)
    {
        GameObject go = Instantiate(pickItemPrefab, new Vector3(1000,1000), Quaternion.identity);
        go.GetComponent<PickItems>().SetItem(itemBundle[itemBundle.FindIndex(a => a.itemID == indexkey)]);
        Item a = go.GetComponent<PickItems>().GetItem();
        Destroy(go);
        return a;
    }   
}
