using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class inventory : MonoBehaviour
{
    public static inventory instance;

    public List<Item> items = new List<Item>();  // 가지고있는 인벤토리 리스트
    public List<Item> equip = new List<Item>();  // 현재 착용중인 장비칸 리스트
    public int playerCoin;  // 보유골드
    public TextMeshProUGUI CoinText;

    public delegate void OnChangeItem();  // 인벤UI 정리용
    public OnChangeItem onChangeItem;

    public delegate void OnChangeEquip(); // 장비칸UI 정리용
    public OnChangeEquip onChangeEquip;

    public bool[] UseCheck = new bool[6];  // 지속효과 아이템 쿨타임여부 확인
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    private void OnEnable()
    {
        StartCoroutine(ShowCoinText());
    }

    private void Start()
    {
        for(int i = 0; i< UseCheck.Length; i++)
        {
            UseCheck[i] = true;
        }
    }
    private void Update()  // 지속효과 아이템의 경우 쿨타임마다 사용을 위하여 
    {
        for (int i = 0; i < equip.Count; i++)
        {
            if (UseCheck[i]&& equip[i].itemType == ItemType.Lasting && !GameManager.Instance.statusImage.activeSelf)
            {
                StartCoroutine(UseItem(equip[i], i));
            }
        }
    }

    public bool AddItem(Item _item)  // 아이템 습득
    {
        if (items.Count < 25)
        {
            items.Add(_item);

            /* 장비 아이템 전용 사운드 */
            SoundManager.Instance.itemGetSound.Play();

            if(onChangeItem != null)
                onChangeItem.Invoke();  // ui drawing.
            return true;
        }

        return false;
    }
    public bool EquipItem(Item _item) // 아이템 장착
    {
        if (equip.Count < 6)
        {
            _item.canUse = _item.Use();
            equip.Add(_item);
            if(onChangeEquip != null)
                onChangeEquip.Invoke(); // ui drawing.
            return true;
        }
        return false;
    }
    public void RemoveItem(int _index) // 아이템 제거
    {
        items.RemoveAt(_index);

        if (onChangeItem != null)
            onChangeItem.Invoke();  // ui drawing.
    }
    public void Unequip(int _index)  // 아이템 장착해제
    {
        equip.RemoveAt(_index);

        if (onChangeEquip != null)
            onChangeEquip.Invoke(); // ui drawing.
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PickItem"))
        {
            PickItems pickItems = collision.GetComponent<PickItems>();
            if (pickItems.item.itemType == ItemType.Consumables)  // 소비아이템의 경우 즉시 사용하도록 만듬, 좀더 간편한 컨트롤 방식을 위하여 이렇게 구현함
            {
                SoundManager.Instance.itemGetSound.Play();
                pickItems.item.Use();
                pickItems.DestoryItem();
                return;
            }
            if (AddItem(pickItems.GetItem()))
                pickItems.DestoryItem();
        }

        if (collision.CompareTag("ItemBox"))
        {
            ItemBox itembox = collision.GetComponent<ItemBox>();
            if(items.Count < 25)
            {
                Destroy(collision);
                itembox.DropItem();
            }
        }
    }

    public void GetOrGiveCoin(int _coin)  // 골드획득
    {
        playerCoin += _coin;
        if (playerCoin < 0)
            playerCoin = 0;
        if(_coin != 0)
        {
            StopCoroutine("CoinTextChange");
            StartCoroutine(CoinTextChange(_coin));
        }
    }

    IEnumerator CoinTextChange(int _coin)
    {
        if (_coin > 0)
        {
            CoinText.text = playerCoin + "<color=blue>+" + _coin + "</color>";
        }
        if (_coin < 0)
        {
            CoinText.text = playerCoin + "<color=red>-" + _coin + "</color>";
        }
        yield return new WaitForSeconds(1.5f);
        CoinText.text = + playerCoin + "";
    }

    IEnumerator ShowCoinText()
    {
        while(true)
        {
            CoinText.text = playerCoin.ToString();
            yield return null;
        }
    }

    IEnumerator UseItem(Item _item , int index)
    {
        UseCheck[index] = _item.Use();
        
        yield return new WaitForSeconds(_item.coolTime);
        UseCheck[index] = true;
        
    }
}
