using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEffect : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    Transform buttonScale;
    Vector3 defaultScale;

    private void Awake()
    {
        buttonScale = transform;

        defaultScale = transform.localScale;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}
