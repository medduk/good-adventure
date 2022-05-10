using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SHOPInformation : MonoBehaviour
{
    public TextMeshProUGUI infoItemName;
    public TextMeshProUGUI itemInformation;
    public Text canbuytext;
    public Image icon;
    public int slotnum;
    public bool canbuy;
    public Button button;

    string showcolor;

    public Item item;   // SHOP스크립트로 부터 정보를 받아 왔음.

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
    public void Show()  // 클릭했을 때 띄워지는 창의 정보.
    {
        showcolor = Changecolor();
        icon.sprite = item.itemImage;
        itemInformation.text = "<color=white>" + item.itemDescription + "</color>";
        infoItemName.text = "<color=" + showcolor + ">" + item.itemName + "</color>";

        if (canbuy)
            canbuytext.text = "구매";
        else
            canbuytext.text = "골드부족";

        if(inventory.instance.items.Count == 25)    // 인벤토리가 꽉 차면.
        {
            canbuy = false;
            canbuytext.text = "가방꽉참";
        }
        button.gameObject.SetActive(false);
    }
    private string Changecolor()    // 등급에 따라 텍스트 변경.
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

    public void Close()
    {
        button.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
    public void BUY()
    {
        if (canbuy)
        {
            SoundManager.Instance.buySound.Play();

            if(item.itemType == ItemType.Consumables)
            {
                item.Use();
                SoundManager.Instance.itemGetSound.Play();
            }
            else
            {
                inventory.instance.AddItem(ItemBundle.instance.MakeItem(item.itemID));
            }
            SHOP.Instance.SellEnd(slotnum);
            inventory.instance.GetOrGiveCoin(-item.price);
            button.gameObject.SetActive(true);
            gameObject.SetActive(false);

        }   
    }
}
