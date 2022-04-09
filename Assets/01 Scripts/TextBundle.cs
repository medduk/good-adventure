using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBundle : MonoBehaviour
{
    Dictionary<int, string[]> textBundle;
    Dictionary<int, Sprite> portraitData;

    public Sprite[] portraitArr;
    private void Awake()
    {
        textBundle = new Dictionary<int, string[]>();
        portraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }
    void GenerateData()
    {
        textBundle.Add(1, new string[] { "안녕하세요 !:n:???", "ㅁ..뭐?:0:주인공", "여기는 튜토리얼 입니다 !:n:???", "잠깐만 ! :7:주인공", " 가만히 있으면 자동공격을 한답니다. :n:???", " 아니... :2:주인공" , "앞의 적을 해치우세요.:n:???" });
        textBundle.Add(2, new string[] { "좋아요 소질이 있어요 ! :n:???", "헤헤:4:주인공", "좀더 앞으로 가보도록 해요 ! :n:???" });
        textBundle.Add(3, new string[] { "기본적인 공격은 벽을 통과되지 않는답니다.:n:???", "응 알겠어:1:주인공", "벽 너머의 적을 공격 해보죠 !:n:???" });

        portraitData.Add(0, portraitArr[0]);
        portraitData.Add(1, portraitArr[1]);
        portraitData.Add(2, portraitArr[2]);
        portraitData.Add(3, portraitArr[3]);
        portraitData.Add(4, portraitArr[4]);
        portraitData.Add(5, portraitArr[5]);
        portraitData.Add(6, portraitArr[6]);
        portraitData.Add(7, portraitArr[7]);
    }

    public string GetText(int id, int textIndex)
    {
        if (textIndex == textBundle[id].Length)
            return null;
        else
            return textBundle[id][textIndex];
    }
    public Sprite GetPortrait(int id)
    {
        return portraitData[id];
    }
}
