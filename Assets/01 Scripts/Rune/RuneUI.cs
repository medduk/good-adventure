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
    int runestoneCount = 5;
    int level;


    public int RunestoneCount
    {
        get
        {
            return runestoneCount;
        }
    }

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
        runestoneCountchange();
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
        if(runestoneCount > 0)
        {
            level = PlayerPrefs.GetInt(System.Enum.GetName(typeof(Runes), runes));
            PlayerPrefs.SetInt(System.Enum.GetName(typeof(Runes), runes), level+1);
            runestoneCount--;
            runestoneCountchange();
        }
    }
    void runestoneCountchange()
    {
        Count.text = +runestoneCount + "";

        if(runestoneCount > 99)
        {
            Count.text = "99+";
        }
    }


}
