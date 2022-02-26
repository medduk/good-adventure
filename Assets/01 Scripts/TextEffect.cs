using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextEffect : MonoBehaviour
{
    public GameObject EndCursor;
    string targetText;
    public float CharPerSeconds;
    Text msgText;
    int index;
    float interval;

    public bool effectcheck;

    public void SetText(string text)
    {
        targetText = text;
        EffectStart();
    }

    private void Awake()
    {
        msgText = GetComponent<Text>();
    }


    void EffectStart()
    {
        effectcheck = true;
        EndCursor.SetActive(false);
        msgText.text = "";
        index = 0;
        interval = 1.0f / CharPerSeconds;
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

        Invoke("Effecting", 1 / CharPerSeconds);
    }
    void EffectEnd()
    {
        effectcheck = false;
        EndCursor.SetActive(true);
    }
    public void EffectfastEnd()
    {
        msgText.text = targetText;
    }
}
