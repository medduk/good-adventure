using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/* ��縦 �ٲ� �ִ�(�Ų����� �̾���� ����) ��ũ��Ʈ. */
public class DialogManager : MonoBehaviour
{
    public TextBundle textBundle;
    public GameObject dialogPanel;
    //public GameObject scanObject;
    public TextEffect talk; // TextEffect ��ũ��Ʈ�� �ִ´�.
    public Image portraitImg;

    public bool isAction;
    public int textIndex;
    public int tutoNumber = 0;

    private string textData;    // TextEffect���� ����ϱ� ���� ���ڿ�.

    private int id;
    private bool isNPC;

    private static DialogManager instance;
    public static DialogManager Instance
    {
        get
        {
            if (instance == null) return null;
            return instance;
        }
    }

    private void Awake()
    {
        if(instance == null)
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
        dialogPanel.SetActive(false);
    }

    public void DialogNext()
    {
        if (!talk.effectCheck)  // �� ���� �� ������ �ִ� ���°� �ƴ϶��, �� �̹� ���� �� ���´ٸ�(�� ��µ� �Ķ��)
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
                textIndex = 0;  // Ȥ�ø��� �ѹ� �� �ʱ�ȭ.
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
            talk.EffectFastEnd();
    }

    public void Action(GameObject scanObj, int plusID = 0)  // �ݶ��̴��� �ε��� �� �ߵ� �Ǿ� ��簡 ���۵�. �� ��ȭ�� ���۽�Ű�� ù �Լ���.
        /*plusID�� npc�� ���� ���� ��ȭ�� �� �� �����Ƿ� � ��ȭ�� ���� ����.*/
    {
        textIndex = 0;  // Dialog�Լ����� ������ �ؽ�Ʈ�� �������� Ȯ����.
        //scanObject = scanObj;
        ObjNumber objNumber = scanObj.GetComponent<ObjNumber>();
        id = objNumber.id + plusID; // npc�� id + �ش� npc�� �� ��ȣ�� ��ȭ ����� �����Ѵ�.
        isNPC = objNumber.isNPC;    // ���� Ȯ�强�� ���� �ڵ�.
        Dialog(id,isNPC);
    }

    void Dialog(int id, bool isNpc) // TextBundle���� ��縦 �����´�.
    {
        if (id == 1)    // id 1�� Ʃ�丮���̴�.
        {
            textData = textBundle.GetText(id + tutoNumber, textIndex);
            tutoNumber++;
        }
        else
        {
            textData = textBundle.GetText(id , textIndex);
        }

        if(textData.Split(':')[1] == "n")   // ��ȭ�ϴ� �߿� �̹����� ���� ����.
        {
            portraitImg.color = new Color(1, 1, 1, 0);  // �����ϰ� ��.
        }
        else // ǥ���� �̹����� �ִٸ�
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
        isAction = true;    // ���� ��ȭ�� ������.
        dialogPanel.SetActive(isAction);
        textIndex++;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isAction == true)
        {
            DialogNext();
        }
    }
}
