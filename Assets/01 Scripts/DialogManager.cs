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
            if (isNPC)
            {
                talk.SetText(textData);
            }
            else
            {
                talk.SetText(textData);
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


        if (isNpc)
        {
            talk.SetText(textData);
        }
        else
        {
            talk.SetText(textData);
        }
        isAction = true;
        dialogPanel.SetActive(isAction);
        textIndex++;
    }
}
