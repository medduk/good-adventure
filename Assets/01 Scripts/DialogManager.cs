using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/* 대사를 바꿔 주는(매끄럽게 이어나가기 위한) 스크립트. */
public class DialogManager : MonoBehaviour
{
    public TextBundle textBundle;
    public GameObject dialogPanel;
    //public GameObject scanObject;
    public TextEffect talk; // TextEffect 스크립트를 넣는다.
    public Image portraitImg;

    public bool isAction;
    public int textIndex;
    public int tutoNumber = 0;

    private string textData;    // TextEffect에서 출력하기 위한 문자열.

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
        if (!talk.effectCheck)  // 한 글자 씩 나오고 있는 상태가 아니라면, 즉 이미 말을 다 끝냈다면(다 출력된 후라면)
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
                textIndex = 0;  // 혹시몰라 한번 더 초기화.
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

    public void Action(GameObject scanObj, int plusID = 0)  // 콜라이더가 부딪힐 때 발동 되어 대사가 시작됨. 즉 대화를 시작시키는 첫 함수임.
        /*plusID는 npc가 여러 개의 대화를 할 수 있으므로 어떤 대화를 할지 결정.*/
    {
        textIndex = 0;  // Dialog함수에서 가져온 텍스트가 끝났는지 확인함.
        //scanObject = scanObj;
        ObjNumber objNumber = scanObj.GetComponent<ObjNumber>();
        id = objNumber.id + plusID; // npc의 id + 해당 npc의 말 번호로 대화 출력을 결정한다.
        isNPC = objNumber.isNPC;    // 추후 확장성을 위한 코드.
        Dialog(id,isNPC);
    }

    void Dialog(int id, bool isNpc) // TextBundle에서 대사를 가져온다.
    {
        if (id == 1)    // id 1은 튜토리얼이다.
        {
            textData = textBundle.GetText(id + tutoNumber, textIndex);
            tutoNumber++;
        }
        else
        {
            textData = textBundle.GetText(id , textIndex);
        }

        if(textData.Split(':')[1] == "n")   // 대화하는 중에 이미지가 없는 거임.
        {
            portraitImg.color = new Color(1, 1, 1, 0);  // 투명하게 함.
        }
        else // 표시할 이미지가 있다면
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
        isAction = true;    // 현재 대화가 진행중.
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
