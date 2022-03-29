using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour
{
    public Item item;
    [SerializeField] int[] dropItemId;
    [SerializeField] int[] dropIChance;
    private int sum = 0;
    public Sprite Openbox;


    private void Awake()
    {
        for (int c = 0; c < dropItemId.Length; c++)
        {
            sum += dropIChance[c];
        }
    }
    // Start is called before the first frame update

    public void DropItem()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = Openbox;
        int DropIndex = SelectItem();
        item = ItemBundle.instance.makeItem(dropItemId[DropIndex]);
        ItemInformation.Instance.item = this.item;
        ItemInformation.Instance.Show();
        GameManager.Instance.OpenItemInformation(false);
        inventory.instance.AddItem(this.item);
    }
    private int SelectItem()
    {
        int randomIndex = Random.Range(1, sum + 1);

        int i = 0;
        while (i < dropItemId.Length)
        {
            randomIndex = randomIndex - dropIChance[i];
            if (randomIndex <= 0)
            {
                break;
            }
            i++;
        }
        return i;
    }
}
