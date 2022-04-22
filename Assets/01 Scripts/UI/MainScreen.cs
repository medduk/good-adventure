using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MainScreen : MonoBehaviour, IPointerDownHandler
{
    public TextMeshProUGUI Text;
    public CanvasGroup mainGroup;
    public Image image;


    private void Start()
    {
        StartCoroutine("Logoshow");
    }
    IEnumerator Logoshow()
    {
        while(image.color.a < 1.0f)
        {
            image.color = new Color(1.0f, 1.0f, 1.0f, image.color.a + Time.deltaTime / 2.5f);
            yield return null;
        }
        Text.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        StartCoroutine("textOutshow");
    }
    IEnumerator textInshow()
    {

        while (Text.color.a < 1.0f)
        {
            Text.color = new Color(1.0f, 1.0f, 1.0f, Text.color.a + Time.deltaTime /2.0f);
            yield return null;
        }
        StartCoroutine("textOutshow");
        
    }
    IEnumerator textOutshow()
    {

        while (Text.color.a > 0f)
        {
            Text.color = new Color(1.0f, 1.0f, 1.0f, Text.color.a - Time.deltaTime / 2.0f);
            yield return null;
        }
        StartCoroutine("textInshow");

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Text.gameObject.activeSelf == true) {
            image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            StopCoroutine("Logoshow");
            StopCoroutine("textInshow");
            StopCoroutine("textOutshow");
            Text.gameObject.SetActive(false);
            mainGroup.alpha = 1;
            mainGroup.interactable = true;
            mainGroup.blocksRaycasts = true;
        }
    }
}
