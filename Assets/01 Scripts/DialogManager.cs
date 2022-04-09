using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DialogManager : MonoBehaviour
{
    public TextBundle textBundle;
    public GameObject dialogPanel;
    public GameObject scanObject;
    public TextEffect talk;
    public Image portraitImg;

    public bool isAction;
    public int textIndex;
    public int tutoNumber = 0;
    private string textData;

    private int id;
    private bool isNPC;


    private void Start()
    {
        dialogPanel.SetActive(false);
    }
    public void DialogNext()
    {
        if (!talk.effectcheck)
        {
            if (id == 1)
            {
                textData = textBundle.GetText(id + tutoNumber - 1, textIndex);
            }
            else
            {
                textData = textBundle.GetText(id, textIndex);
            }

            if (textData == null)
            {
                isAction = false;
                dialogPanel.SetActive(isAction);
                textIndex = 0;
                return;
            }
            if (textData.Split(':')[1] == "n")
            {
                portraitImg.color = new Color(1, 1, 1, 0);
            }
            else
            {
                portraitImg.sprite = textBundle.GetPortrait(int.Parse(textData.Split(':')[1]));
                portraitImg.color = new Color(1, 1, 1, 1);
            }

            if (isNPC)
            {
                talk.SetText(textData.Split(':')[0]);
                talk.namechange(textData.Split(':')[2]);
            }
            else
            {
                talk.SetText(textData.Split(':')[0]);
                talk.namechange(textData.Split(':')[2]);
            }
            isAction = true;
            textIndex++;
        }
        else
            talk.EffectfastEnd();
    }

    public void Action(GameObject scanObj)
    {
        textIndex = 0;
        scanObject = scanObj;
        ObjNumber objNumber = scanObject.GetComponent<ObjNumber>();
        id = objNumber.id;
        isNPC = objNumber.isNPC;
        Dialog(id,isNPC);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isAction == true)
        {
            DialogNext();
        }
            
    }
    void Dialog(int id, bool isNpc)
    {
        if (id == 1)
        {
            textData = textBundle.GetText(id + tutoNumber, textIndex);
            tutoNumber++;
        }
        else
        {
            textData = textBundle.GetText(id , textIndex);
        }

        if(textData.Split(':')[1] == "n")
        {
            portraitImg.color = new Color(1, 1, 1, 0);
        }
        else
        {
            portraitImg.sprite = textBundle.GetPortrait(int.Parse(textData.Split(':')[1]));
            portraitImg.color = new Color(1, 1, 1, 1);
        }

        if (isNpc)
        {
            talk.SetText(textData.Split(':')[0]);
            talk.namechange(textData.Split(':')[2]);
            
        }
        else
        {
            talk.SetText(textData.Split(':')[0]);
            talk.namechange(textData.Split(':')[2]);
        }
        isAction = true;
        dialogPanel.SetActive(isAction);
        textIndex++;
    }
}
