using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* ��ȭ ���ڸ� ����ִ� ��ũ��Ʈ. */
public class TextEffect : MonoBehaviour
{
    public GameObject EndCursor;    // ������ �� ȭ��ǥ�� �ִ�.
    string targetText;
    public float CharPerSeconds;    // �� ���ھ� ��� ��Ű�� �ӵ�.
    Text msgText;
    [SerializeField] Text sayname;
    int index;  // ������ �� �����ΰ�.
    float interval;

    public bool effectCheck;    // ���� �� ��簡 �� ���ھ� ������ΰ� Ȯ��.

    public void SetText(string text)    // DialogManager���� �����Ѵ�.
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
