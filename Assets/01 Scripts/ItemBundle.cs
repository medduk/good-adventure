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

}
