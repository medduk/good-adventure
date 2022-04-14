using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RuneUI : MonoBehaviour
{
    public GameObject Runepage;
    public GameObject RuneExplanationpage;
    public Button button;
    public Sprite what, wow;
    public TextMeshProUGUI Count;

    bool IsRunepage = true;
    int RunestoneCount = 5;
    int level;
    // Start is called before the first frame update
    public static RuneUI instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    private void Start()
    {
        Count.text = +RunestoneCount+ ""; 
    }
    public void ChangePage()
    {
        
        if (IsRunepage)
        {
            button.image.sprite = wow;
            Runepage.SetActive(false);
            RuneExplanationpage.SetActive(true);
            IsRunepage = false;
            return;
        }
        if (!IsRunepage)
        {
            button.image.sprite = what;
            Runepage.SetActive(true);
            RuneExplanationpage.SetActive(false);
            IsRunepage = true;
            return;
        }
    }
    public void Levelup(Runes runes)
    {
        if(RunestoneCount > 0)
        {
            level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), runes));
            PlayerPrefs.SetInt(System.Enum.GetName(typeof(Runes), runes), level+1);
            RunestoneCount--;
            Count.text = +RunestoneCount + "";
        }
    }
}
