using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SHOP : MonoBehaviour
{
    public List<Item> Sell = new List<Item>();
    public Button[] BuyButton;
    public GameObject BUY;

    [SerializeField] int[] NormalThing;
    [SerializeField] int[] RareThing;
    [SerializeField] int[] UnigueThing;

    [SerializeField] int[] Chance;
    int sum;
    private static SHOP instance = null;
    public static SHOP Instance
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

        for (int c = 0; c < Chance.Length; c++)
        {
            sum += Chance[c];
        }
    }

    private void Start()
    {
        SellStart();
        BUY.SetActive(true);
        gameObject.SetActive(false);
    }
    public void SellStart()
    {
        Sell.Clear();
        int sellcount = Random.Range(3, 7);
        
        for(int i = 0; i < sellcount; i++)
        {
            int Quality = SellChance();
            switch (Quality)
            {
                case 0:
                    Sell.Add(ItemBundle.instance.makeItem(NormalThing[Random.Range(0, NormalThing.Length)]));
                    break;
                case 1:
                    Sell.Add(ItemBundle.instance.makeItem(RareThing[Random.Range(0, RareThing.Length)]));
                    break;
                case 2:
                    Sell.Add(ItemBundle.instance.makeItem(UnigueThing[Random.Range(0, UnigueThing.Length)]));
                    break;
            }
            BuyButton[i].transform.GetChild(0).GetComponent<Image>().sprite = Sell[i].itemImage;
            BuyButton[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "" + Sell[i].price;
        }
        Redrow();
        Ondrow();
    }

    private int SellChance()
    {
        int randomIndex = Random.Range(1, sum + 1);

        int i = 0;
        while (i < Chance.Length)
        {
            randomIndex = randomIndex - Chance[i];
            if (randomIndex <= 0)
            {
                break;
            }
            i++;
        }
        return i;
    }

    public void openBuyTap(int index)
    {
        if (inventory.instance.Coin < Sell[index].price)
            SHOPInformation.Instance.canbuy = false;
        else if (inventory.instance.Coin >= Sell[index].price)
            SHOPInformation.Instance.canbuy = true;

        SHOPInformation.Instance.item = Sell[index];
        SHOPInformation.Instance.slotnum = index;
        SHOPInformation.Instance.Show();
        BUY.SetActive(true);
    }
    private void Redrow()
    {
        for(int i = 0; i< BuyButton.Length; i++)
        {
            BuyButton[i].GetComponent<CanvasGroup>().alpha = 0;
            BuyButton[i].GetComponent<CanvasGroup>().interactable = false;
            BuyButton[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
            
    }
    private void Ondrow()
    {
        for (int i = 0; i < Sell.Count; i++)
        {
            BuyButton[i].GetComponent<CanvasGroup>().alpha = 1;
            BuyButton[i].GetComponent<CanvasGroup>().interactable = true;
            BuyButton[i].GetComponent<CanvasGroup>().blocksRaycasts = true;
        }
    }
    public void SellEnd(int index)
    {

            BuyButton[index].GetComponent<CanvasGroup>().alpha = 0.5f;
            BuyButton[index].GetComponent<CanvasGroup>().interactable = false;
            BuyButton[index].GetComponent<CanvasGroup>().blocksRaycasts = false;
        

    }
}
