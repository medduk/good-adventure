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
        for(int i = 0; i<3; i++)
        {
            GameObject go = Instantiate(pickItemPrefab, pos[i], Quaternion.identity);
            go.GetComponent<PickItems>().SetItem(itemBundle[Random.Range(0,2)]);
        }
    }
}
