using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* 대화 문자를 띄워주는 스크립트. */
public class TextEffect : MonoBehaviour
{
    public GameObject EndCursor;    // 오른쪽 밑 화살표가 있다.
    string targetText;
    public float CharPerSeconds;    // 한 글자씩 출력 시키는 속도.
    Text msgText;
    [SerializeField] Text sayname;
    int index;  // 문장이 몇 글자인가.
    float interval;

    public bool effectCheck;    // 현재 이 대사가 한 글자씩 출력중인가 확인.

    public void SetText(string text)    // DialogManager에서 세팅한다.
    {
        targetText = text;
        EffectStart();
    }

    private void Awake()
    {
        msgText = GetComponent<Text>();
        interval = 1.0f / CharPerSeconds;
    }

    void EffectStart()
    {
        effectCheck = true;
        EndCursor.SetActive(false);
        msgText.text = "";
        index = 0;
        Invoke("Effecting", interval);
    }
    void Effecting()
    {
        if(msgText.text == targetText)
        {
            EffectEnd();
            return;
        }
        msgText.text += targetText[index];
        index++;

        Invoke("Effecting", interval);
    }
    void EffectEnd()
    {
        effectCheck = false;
        EndCursor.SetActive(true);
    }
    public void EffectFastEnd()
    {
        msgText.text = targetText;
    }
    public void namechange(string name)
    {
        sayname.text = name;
    }
}
