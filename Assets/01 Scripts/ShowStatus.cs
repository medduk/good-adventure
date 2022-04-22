using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShowStatus : MonoBehaviour
{
    public TextMeshProUGUI Text;
    private PlayerStatus status;
    // Start is called before the first frame update
    float[] Now = new float[7];
    float[] New = new float[7];
    string[] C = new string[7]; 
    private void Awake()
    {
        status = PlayerStatus.Instance;
    }
    private void Start()
    {
        New[0] = (float)status.PlayerDamage;
        New[1] = (float)status.PlayerAttackDelay;
        New[2] = (float)status.PlayerDefense;
        New[3] = (float)status.PlayerMoveSpeed;
        New[4] = (float)status.CriticalProbability;
        New[5] = (float)status.CriticalDamage;
        New[6] = (float)status.AbsorptionOfVitality;


        for (int i = 0; i < C.Length; i++)
        {
            C[i] = "black";
        }
        for (int i = 0; i < New.Length; i++)
        {
            Now[i] = New[i];
        }
    }
    void Update()
    {
        if(GameManager.Instance.statusImage.activeSelf == true)
        {
            New[0] = (float)status.PlayerDamage;
            New[1] = (float)status.PlayerAttackDelay;
            New[2] = (float)status.PlayerDefense;
            New[3] = (float)status.PlayerMoveSpeed;
            New[4] = (float)status.CriticalProbability;
            New[5] = (float)status.CriticalDamage;
            New[6] = (float)status.AbsorptionOfVitality;

            ColorChange();

            Text.text = "공격력 : <color="+C[0]+">" +status.PlayerDamage+ "</color>\n" + "공격속도 : <color=" + C[1] + ">" + status.PlayerAttackDelay + "</color>/s\n" + "방어력 : <color=" + C[2] + ">" + status.PlayerDefense + "</color>\n" + "이동속도 : <color=" + C[3] + ">" + status.PlayerMoveSpeed + "</color>\n" + "크리티컬 확률\n<color=" + C[4] + ">" + status.CriticalProbability + "</color>%\n" + "크리티컬 데미지\n<color=" + C[5] + ">" + status.CriticalDamage + "</color>%\n";
            if (status.AbsorptionOfVitality != 0)
                Text.text += "흡혈력 : <color=" + C[6] + ">" + string.Format("{0:F2}", status.AbsorptionOfVitality * 100) + "</color>%"; 
        }        
    }
    void ColorChange()
    {
        for(int i = 0; i < Now.Length; i++)
        {
            if (Now[i] < New[i])
            {
                if (i != 1)
                    C[i] = "blue";
                else
                    C[i] = "red";
            }
            if (Now[i] > New[i])
            {
                if(i != 1)
                    C[i] = "red";
                else
                    C[i] = "blue";
            }
            if (Now[i] == New[i])
                C[i] = "black";

        }
    }
    public void NowChange()
    {
        New[0] = (float)status.PlayerDamage;
        New[1] = (float)status.PlayerAttackDelay;
        New[2] = (float)status.PlayerDefense;
        New[3] = (float)status.PlayerMoveSpeed;
        New[4] = (float)status.CriticalProbability;
        New[5] = (float)status.CriticalDamage;
        New[6] = (float)status.AbsorptionOfVitality;

        for (int i = 0; i < New.Length; i++)
        {
            Now[i] = New[i];
        }

    }
}
