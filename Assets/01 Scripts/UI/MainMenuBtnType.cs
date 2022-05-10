using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class MainMenuBtnType : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    public BTNType currentType;
    public Transform buttonScale;
    Vector3 defaultScale;

    public TextMeshProUGUI skipbutton, Soundbutton;

    [Header("CanvasGroup")]
    public CanvasGroup mainGroup;
    public CanvasGroup OptionGroup;

    [Header("State Images")]
    public Image tutorialSkipStateImage;
    public Image soundStateImage;
    public Color onStateColor;
    public Color offStateColor;

    private void Start()
    {
        defaultScale = buttonScale.localScale;
        InitSkipButtonText();
        InitSoundButtonText();
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
                ContinueDataManager.SetSkip();
                if (ContinueDataManager.skip)
                {
                    skipbutton.text = "[튜토리얼]켜기";
                    tutorialSkipStateImage.color = offStateColor;
                }
                else
                {
                    skipbutton.text = "[튜토리얼]끄기";
                    tutorialSkipStateImage.color = onStateColor;
                }
                SoundManager.Instance.buttonsSound.Play();
                break;
            case BTNType.Sound:
                if (!SoundManager.Instance.GetisMute())
                {
                    SoundManager.Instance.SoundOFF();
                    Soundbutton.text = "[소리]켜기";
                    soundStateImage.color = offStateColor;
                }
                else
                {
                    SoundManager.Instance.SoundON();
                    Soundbutton.text = "[소리]끄기";
                    soundStateImage.color = onStateColor;
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

    public void InitSkipButtonText()
    {
        try
        {
            if (ContinueDataManager.skip)
            {
                skipbutton.text = "[튜토리얼]켜기";
                tutorialSkipStateImage.color = offStateColor;
            }
            else
            {
                skipbutton.text = "[튜토리얼]끄기";
                tutorialSkipStateImage.color = onStateColor;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return;
        }
    }

    public void InitSoundButtonText()
    {
        try
        {
            if (SoundManager.Instance.GetisMute())
            {
                Soundbutton.text = "[소리]켜기";
                soundStateImage.color = offStateColor;
            }
            else
            {
                Soundbutton.text = "[소리]끄기";
                soundStateImage.color = onStateColor;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return;
        }
    }
}
