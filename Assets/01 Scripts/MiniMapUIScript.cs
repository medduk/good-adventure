using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapUIScript : MonoBehaviour
{
    [SerializeField] Camera miniMapCamera;
    [SerializeField] GameObject SmallMap;
    [SerializeField] GameObject BigMap;

    [SerializeField] float bigOrthographicSize = 9f;
    [SerializeField] float smallOrthographicSize = 5f;

    private void Start()
    {
        SmallMap.SetActive(true);
        BigMap.SetActive(false);
    }

    public void ClickSmallToBig()
    {
        BigMap.SetActive(true);
        miniMapCamera.orthographicSize = bigOrthographicSize;
        SmallMap.SetActive(false);
    }

    public void MakeBigToSmall()
    {
        SmallMap.SetActive(true);
        miniMapCamera.orthographicSize = smallOrthographicSize;
        BigMap.SetActive(false);
    }
}
