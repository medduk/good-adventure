using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class inventory : MonoBehaviour
{
    public static inventory instance;
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public List<Item> items = new List<Item>();
    public List<Item> equip = new List<Item>();
    public int Coin;
    public TextMeshProUGUI CoinText;

    public delegate void OnChangeItem();
    public OnChangeItem onChangeItem;

    public delegate void OnChangeEquip();
    public OnChangeEquip onChangeEquip;


    public bool[] UseCheck = new bool[6];

    private void Start()
    {
        for(int i = 0; i< UseCheck.Length; i++)
        {
            UseCheck[i] = true;
        }
    }
    private void Update()
    {
        for (int i = 0; i < equip.Count; i++)
        {
            if (UseCheck[i]&& equip[i].itemType == ItemType.Lasting && !GameManager.Instance.statusImage.activeSelf)
            {
                StartCoroutine(UseItem(equip[i], i));
            }
        }
    }

    public bool AddItem(Item _item)
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
    public bool EquipItem(Item _item)
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
    public void RemoveItem(int _index)
    {
        items.RemoveAt(_index);

        if (onChangeItem != null)
            onChangeItem.Invoke();  // ui drawing.
    }
    public void Unequip(int _index)
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
            if (pickItems.item.itemType == ItemType.Consumables)
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

    public void GetORGiveCoin(int coin)
    {
        Coin += coin;
        if (Coin < 0)
            Coin = 0;
        if(coin != 0)
        {
            StopCoroutine("CoinTextChange");
            StartCoroutine("CoinTextChange",coin);
        }
        
    }

    IEnumerator CoinTextChange(int coin)
    {
        if (coin > 0)
        {
            CoinText.text = Coin + "<color=blue>+" + coin + "</color>";
        }
        if (coin < 0)
        {
            CoinText.text = Coin + "<color=red>-" + coin + "</color>";
        }
        yield return new WaitForSeconds(1.5f);
        CoinText.text = + Coin + "";
    }

    IEnumerator UseItem(Item _item , int index)
    {
        UseCheck[index] = _item.Use();
        
        yield return new WaitForSeconds(_item.coolTime);
        UseCheck[index] = true;
        
    }
}
