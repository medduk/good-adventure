using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class RuneLevelUp : MonoBehaviour, IPointerDownHandler
{
    public GameObject[] rune;
    public Sprite[] runeimage;

    [SerializeField] GameObject UI;
    [SerializeField] int[] RuneChance;
    [SerializeField] Image show, real;
    [SerializeField] Button choose , back;
    private int sum = 0;
    int index;

    private void Start()
    {
        for (int c = 0; c < RuneChance.Length; c++)
        {
            sum += RuneChance[c];
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {   
        if(RuneUI.instance.RunestoneCount > 0)
        {
            index = RuneChoose();
            real.sprite = runeimage[index];
            show.gameObject.SetActive(true);
            real.gameObject.SetActive(false);
            choose.gameObject.SetActive(true);
            back.gameObject.SetActive(false);
            UI.SetActive(true);

        }
    }

    public void chooserune()
    {

        show.gameObject.SetActive(false);
        real.gameObject.SetActive(true);
        choose.gameObject.SetActive(false);
        back.gameObject.SetActive(true);
        rune[index].GetComponent<Rune>().LevelUP();
        

    }
    private int RuneChoose()
    {
        int randomIndex = Random.Range(1, sum + 1);

        int i = 0;
        while (i < RuneChance.Length)
        {
            randomIndex = randomIndex - RuneChance[i];
            if (randomIndex <= 0)
            {
                break;
            }
            i++;
        }
        return i;
    }
    public void CloseUI()
    {
        UI.SetActive(false);
        
    }
}
