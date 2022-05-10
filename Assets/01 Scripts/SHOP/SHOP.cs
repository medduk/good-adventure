using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SHOP : MonoBehaviour
{
    public List<Item> Sell = new List<Item>();  // 파는 아이템 리스트.
    public Button[] BuyButton;  // 아이템 버튼들(구매)
    public GameObject BUY;  // shop information을 띄우기 위한 오브젝트.

    public TextMeshProUGUI playerCoinText;   // 상점에서 플레이어가 가지고 있는 돈을 위한 텍스트.

    [SerializeField] int[] NormalThing;
    [SerializeField] int[] RareThing;
    [SerializeField] int[] UniqueThing;

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
        if (gameObject.activeSelf) gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(ShowCoinText());
    }

    private void Start()
    {
        SellStart();
        BUY.SetActive(true);
        gameObject.SetActive(false);
    }
    public void SellStart()
    {
        Sell.Clear();    // 파는 아이템 리스트를 초기화
        int sellcount = Random.Range(3, 7);     // 물건 갯수 랜덤.
        
        for(int i = 0; i < sellcount; i++)
        {
            int quality = SellChance();
            switch (quality)
            {
                case 0:
                    Sell.Add(ItemBundle.instance.MakeItem(NormalThing[Random.Range(0, NormalThing.Length)]));
                    break;
                case 1:
                    Sell.Add(ItemBundle.instance.MakeItem(RareThing[Random.Range(0, RareThing.Length)]));
                    break;
                case 2:
                    Sell.Add(ItemBundle.instance.MakeItem(UniqueThing[Random.Range(0, UniqueThing.Length)]));
                    break;
            }
            BuyButton[i].transform.GetChild(0).GetComponent<Image>().sprite = Sell[i].itemImage;
            BuyButton[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "" + Sell[i].price;
        }
        Redraw();
        Ondraw();
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

    public void OpenBuyTap(int index)   // 아이템을 클릭 했을 때 구매할 수 있는지 없는지 정보창을 띄워줌.
    {
        if (inventory.instance.playerCoin < Sell[index].price)
            SHOPInformation.Instance.canbuy = false;
        else if (inventory.instance.playerCoin >= Sell[index].price)
            SHOPInformation.Instance.canbuy = true;

        SHOPInformation.Instance.item = Sell[index];
        SHOPInformation.Instance.slotnum = index;
        SHOPInformation.Instance.Show();
        BUY.SetActive(true);
    }
    private void Redraw()
    {
        for(int i = 0; i< BuyButton.Length; i++)
        {
            BuyButton[i].GetComponent<CanvasGroup>().alpha = 0;
            BuyButton[i].GetComponent<CanvasGroup>().interactable = false;
            BuyButton[i].GetComponent<CanvasGroup>().blocksRaycasts = false;
        }
            
    }
    private void Ondraw()
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

    IEnumerator ShowCoinText()
    {
        while(true)
        {
            playerCoinText.text = inventory.instance.playerCoin.ToString();
            yield return null;
        }
    }
}
