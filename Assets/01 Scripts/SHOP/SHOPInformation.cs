using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SHOPInformation : MonoBehaviour
{
    public TextMeshProUGUI name;
    public TextMeshProUGUI text;
    public Text canbuytext;
    public Image icon;
    public int slotnum;
    public bool canbuy;
    public Button button;

    string showcolor;

    public Item item;   // SHOP��ũ��Ʈ�� ���� ������ �޾� ����.

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
    public void Show()  // Ŭ������ �� ������� â�� ����.
    {
        showcolor = Changecolor();
        icon.sprite = item.itemImage;
        text.text = "<color=white>" + item.itemDescription + "</color>";
        name.text = "<color=" + showcolor + ">" + item.itemName + "</color>";

        if (canbuy)
            canbuytext.text = "����";
        else
            canbuytext.text = "������";

        if(inventory.instance.items.Count == 25)    // �κ��丮�� �� ����.
        {
            canbuy = false;
            canbuytext.text = "�������";
        }
        button.gameObject.SetActive(false);
    }
    private string Changecolor()    // ��޿� ���� �ؽ�Ʈ ����.
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
                inventory.instance.AddItem(ItemBundle.instance.makeItem(item.itemID));
            }
            SHOP.Instance.SellEnd(slotnum);
            inventory.instance.GetOrGiveCoin(-item.price);
            button.gameObject.SetActive(true);
            gameObject.SetActive(false);

        }   
    }
}
