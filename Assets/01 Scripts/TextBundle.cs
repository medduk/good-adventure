using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBundle : MonoBehaviour
{
    Dictionary<int, string[]> textBundle;

    private void Awake()
    {
        textBundle = new Dictionary<int, string[]>();
        GenerateData();
    }
    void GenerateData()
    {
        textBundle.Add(1, new string[] { "안녕하세요 !", "여기는 튜토리얼 입니다 ! ", " 가만히 있으면 자동공격을 한답니다. ","앞의 적을 해치우세요." });
        textBundle.Add(2, new string[] { "좋아요 소질이 있어요 ! ", "좀더 앞으로 가보도록 해요 ! " });
        textBundle.Add(3, new string[] { "기본적인 공격은 벽을 통과되지 않는답니다.","벽 너머의 적을 공격 해보죠 !" });
    }

    public string GetText(int id, int textIndex)
    {
        if (textIndex == textBundle[id].Length)
            return null;
        else
            return textBundle[id][textIndex];
    }
}
