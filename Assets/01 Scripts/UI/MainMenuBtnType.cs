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
    public CanvasGroup UpgradeGroup;
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
                CanvasGroupOff(UpgradeGroup);
                CanvasGroupOff(OptionGroup);
                CanvasGroupOff(mainGroup);

                SoundManager.Instance.buttonsSound.Play();
                break;
            case BTNType.Upgrade:
                CanvasGroupOn(UpgradeGroup);
                CanvasGroupOff(RuneUI);
                CanvasGroupOff(mainGroup);
                CanvasGroupOff(StartGroup);

                SoundManager.Instance.buttonsSound.Play();
                break;
            case BTNType.New:
                SceneManager.LoadScene("Loading");

                SoundManager.Instance.buttonsSound.Play();
                break;
            case BTNType.Option:
                CanvasGroupOn(OptionGroup);
                CanvasGroupOff(UpgradeGroup);
                CanvasGroupOff(StartGroup);
                CanvasGroupOff(mainGroup);
                SoundManager.Instance.buttonsSound.Play();
                break;
            case BTNType.Rune:
                CanvasGroupOn(RuneUI);
                CanvasGroupOff(mainGroup);
                CanvasGroupOff(UpgradeGroup);
                CanvasGroupOff(StartGroup);

                SoundManager.Instance.buttonsSound.Play();
                break;
            case BTNType.SoundON:

                SoundManager.Instance.buttonsSound.Play();
                break;
            case BTNType.SoundOFF:

                SoundManager.Instance.buttonsSound.Play();
                break;
            case BTNType.Back:
                CanvasGroupOn(mainGroup);
                CanvasGroupOff(OptionGroup);
                CanvasGroupOff(StartGroup);

                SoundManager.Instance.buttonsSound.Play();
                break;
            case BTNType.GameBack:
                CanvasGroupOn(StartGroup);
                CanvasGroupOff(UpgradeGroup);
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
}
