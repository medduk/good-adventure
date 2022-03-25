using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInformation : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Text;
    public Image icon;
    public int slotnum;
    string showcolor;

    public Item item;

    private static ItemInformation instance = null;

    public static ItemInformation Instance
    {
        get
        {
            if (instance != null) return instance;
            return null;
        }
    }

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void Show()
    {
        showcolor = changecolor();
        icon.sprite = item.itemImage;
        Text.text = "<color=white>" + item.itemDescription + "</color>";
        Name.text = "<color=" + showcolor + ">" + item.itemName + "</color>";
    }
    private string changecolor()
    {
        if (item.level == 2)
            return "blue";
        if (item.level == 3)
            return "green";
        if (item.level == 4)
            return "yellow";
        if (item.level == 5)
            return "red";
        if (item.level == 6)
            return "#d95b9a";
        return "white";
    }
    public void close()
    {
        gameObject.SetActive(false);
    }
    public void Remove()
    {
        inventory.instance.RemoveItem(slotnum);
        gameObject.SetActive(false);
    }
}
