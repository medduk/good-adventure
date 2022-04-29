using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class MainMenuBtnType : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public BTNType currentType;
    public Transform buttonScale;
    Vector3 defaultScale;
    public CanvasGroup mainGroup;
    public CanvasGroup OptionGroup;
    public TextMeshProUGUI skipbutton, Soundbutton;

    bool sound = true;
    private void Start()
    {
        defaultScale = buttonScale.localScale;
        SkipButtonText();
    }

    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BTNType.Start:
                SceneManager.LoadScene("Loading");
                SoundManager.Instance.buttonsSound.Play();
                break;
            case BTNType.Option:
                CanvasGroupOn(OptionGroup);
                CanvasGroupOff(mainGroup);
                SoundManager.Instance.buttonsSound.Play();
                break;
            case BTNType.Skip:
                ContinueDataManager.Setskip();
                if (ContinueDataManager.skip)
                    skipbutton.text = "Æ©Åä¸®¾ó OFF";
                else
                    skipbutton.text = "Æ©Åä¸®¾ó ON";
                    SoundManager.Instance.buttonsSound.Play();
                break;
            case BTNType.Sound:
                if (sound)
                {
                    sound = false;
                    SoundManager.Instance.SoundOFF();
                    Soundbutton.text = "¼Ò¸®ÄÑ±â";
                }
                else
                {
                    sound = true;
                    SoundManager.Instance.SoundON();
                    Soundbutton.text = "¼Ò¸®²ô±â";
                }
                SoundManager.Instance.buttonsSound.Play();
                break;
            case BTNType.Back:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(OptionGroup);

                SoundManager.Instance.buttonsSound.Play();
                break;
            case BTNType.Quit:
                SoundManager.Instance.buttonsSound.Play();
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

    public void SkipButtonText()
    {
        try
        {
            if (ContinueDataManager.skip)
                skipbutton.text = "Æ©Åä¸®¾ó OFF";
            else
                skipbutton.text = "Æ©Åä¸®¾ó ON";
        }
        catch (Exception ex)
        {
            return;
        }
    }
}
