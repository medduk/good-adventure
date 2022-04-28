using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SHOPInformation : MonoBehaviour
{
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Text;
    public Text canbuytext;
    public Image icon;
    public int slotnum;
    public bool canbuy;

    string showcolor;

    public Item item;

    private static SHOPInformation instance = null;

    public static SHOPInformation Instance
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

        if (canbuy)
            canbuytext.text = "±¸¸Å";
        else
            canbuytext.text = "°ñµåºÎÁ·";

        if(inventory.instance.items.Count == 25)
        {
            canbuy = false;
            canbuytext.text = "°¡¹æ²ËÂü";
        }
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
    public void BUY()
    {
        if (canbuy)
        {
            if(item.itemType == ItemType.Consumables)
            {
                item.Use();
                SoundManager.Instance.itemGetSound.Play();
            }
            else
            {
                inventory.instance.AddItem(ItemBundle.instance.makeItem(item.itemID));
            }
            SHOP.Instance.SellEnd(slotnum);
            inventory.instance.GetORGiveCoin(-item.price);
            gameObject.SetActive(false);
        }   
    }
}
