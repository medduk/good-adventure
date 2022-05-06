using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 대화 정보를 담고 있는 스크립트. */
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
        textBundle.Add(4, new string[] { "잘하셨어요 ! 튜토리얼은 여기까지 입니다 !:n:???", "벌써 끝이야 ?:7:주인공", "이제 포탈을 타고 본격적인 모험을 떠나보세요 ! :n:???" ,"튜토리얼은 옵션에서 켜고 끌 수 있답니다. :n:???" });  // 재원 추가.
        textBundle.Add(1000, new string[] { "어서와, 전부 맡기라고.:9:대장장이", "안녕하세요! :4:주인공"});
        textBundle.Add(1001, new string[] { "돈은??:9:대장장이", "...:7:주인공", "농담이야:12:대장장이" });
        textBundle.Add(2000, new string[] { ".....:16:상점주인", "안녕하세요...? :0:주인공" });
        textBundle.Add(2001, new string[] { "ㄱ..ㄱ..:23:상점주인", "에....?:6:주인공", "감사합니다 !!!:20:상점주인", "깜짝이야 !!:3:주인공" });
        textBundle.Add(30000, new string[] { "미지의 힘을 확인하러 오셧나요?:n:거대룬", "행운을 빕니다.:n:거대룬" });
        textBundle.Add(30001, new string[] { "미지의 힘이 도움이 되길.:n:거대룬",});
        textBundle.Add(31000, new string[] { "모험을 떠난지 얼마 안됐는데..:1:주인공", "수상한 문을 발견했어..:1:주인공", " 히얍 ! :5:주인공" });
        textBundle.Add(31001, new string[] { "좋아 가볼까 ! :4:주인공", });
        // 표정: 8로 나누었을때 나머지가 ( 0 : 무표정 , 1 : 기본표정 , 2: 정색 , 3: 외침 , 4: 웃음 , 5: 화남, 6 : 슬픈 , 7 : 당황 )
        //주인공
        portraitData.Add(0, portraitArr[0]);
        portraitData.Add(1, portraitArr[1]);
        portraitData.Add(2, portraitArr[2]);
        portraitData.Add(3, portraitArr[3]);
        portraitData.Add(4, portraitArr[4]);
        portraitData.Add(5, portraitArr[5]);
        portraitData.Add(6, portraitArr[6]);
        portraitData.Add(7, portraitArr[7]);
        //대장장이
        portraitData.Add(8, portraitArr[8]);
        portraitData.Add(9, portraitArr[9]);
        portraitData.Add(10, portraitArr[10]);
        portraitData.Add(11, portraitArr[11]);
        portraitData.Add(12, portraitArr[12]);
        portraitData.Add(13, portraitArr[13]);
        portraitData.Add(14, portraitArr[14]);
        portraitData.Add(15, portraitArr[15]);
        //상점
        portraitData.Add(16, portraitArr[16]);
        portraitData.Add(17, portraitArr[17]);
        portraitData.Add(18, portraitArr[18]);
        portraitData.Add(19, portraitArr[19]);
        portraitData.Add(20, portraitArr[20]);
        portraitData.Add(21, portraitArr[21]);
        portraitData.Add(22, portraitArr[22]);
        portraitData.Add(23, portraitArr[23]);
    }

    public string GetText(int id, int textIndex)
    {
        if (textIndex == textBundle[id].Length) // 현재 문장에서 textBundle이 가지고 있는 대화가 끝났으므로 대화가 없다는 뜻의 null반환.
            return null;
        else
            return textBundle[id][textIndex];
    }
    public Sprite GetPortrait(int id)   // 어떤 표정인지 결정.
    {
        return portraitData[id];
    }
}
