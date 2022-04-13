using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class MainMenuBtnType : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public BTNType currentType;
    public Transform buttonScale;
    Vector3 defaultScale;
    public CanvasGroup mainGroup;
    public CanvasGroup StartGroup;
    public CanvasGroup OptionGroup;
    public CanvasGroup RuneUI;
    private void Start()
    {
        defaultScale = buttonScale.localScale;
    }
    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNType.Start:
                CanvasGroupOn(StartGroup);
                CanvasGroupOff(OptionGroup);
                CanvasGroupOff(mainGroup);
                break;
            case BTNType.Upgrade:
                CanvasGroupOn(OptionGroup);
                CanvasGroupOff(RuneUI);
                CanvasGroupOff(mainGroup);
                CanvasGroupOff(StartGroup);
                break;
            case BTNType.New:
                SceneManager.LoadScene("Loading");
                break;
            case BTNType.Continue:

                break;
            case BTNType.Rune:
                CanvasGroupOn(RuneUI);
                CanvasGroupOff(mainGroup);
                CanvasGroupOff(OptionGroup);
                CanvasGroupOff(StartGroup);
                break;
            case BTNType.SoundOFF:

                break;
            case BTNType.Back:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(OptionGroup);
                CanvasGroupOff(StartGroup);
                break;
            case BTNType.Quit:
                Application.Quit();
                break;
        }
    }

    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    public void CanvasGroupOff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
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
