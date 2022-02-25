using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    private TextMeshProUGUI text;

    private WaitForSeconds sec;
    public float fadeTime = 0.1f;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        StartCoroutine(FadeText());
    }

    private void Update()
    {

    }

    IEnumerator FadeText()
    {
        sec = new WaitForSeconds(fadeTime);
        while (true)
        {
            while (text.color.a < 1)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + Time.deltaTime);
                Debug.Log("µÅ°í ÀÖ¾î?");
                yield return sec;
            }
            while (text.color.a > 0.1)
            {
                text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - Time.deltaTime);
                Debug.Log("µÇ°í ÀÖÂÄ!!!!");
                yield return sec;
            }
        }
    }
}
