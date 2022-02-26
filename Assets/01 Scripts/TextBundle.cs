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
        textBundle.Add(1, new string[] { "�ȳ��ϼ��� !", "����� Ʃ�丮�� �Դϴ� ! ", " ������ ������ �ڵ������� �Ѵ�ϴ�. ","���� ���� ��ġ�켼��." });
        textBundle.Add(2, new string[] { "���ƿ� ������ �־�� ! ", "���� ������ �������� �ؿ� ! " });
        textBundle.Add(3, new string[] { "�⺻���� ������ ���� ������� �ʴ´�ϴ�.","�� �ʸ��� ���� ���� �غ��� !" });
    }

    public string GetText(int id, int textIndex)
    {
        if (textIndex == textBundle[id].Length)
            return null;
        else
            return textBundle[id][textIndex];
    }
}
