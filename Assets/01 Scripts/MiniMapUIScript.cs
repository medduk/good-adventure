using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapUIScript : MonoBehaviour
{
    [SerializeField] Camera miniMapCamera;
    [SerializeField] GameObject SmallMap;
    [SerializeField] GameObject BigMap;

    private void Start()
    {
        SmallMap.SetActive(true);
        BigMap.SetActive(false);
    }

    public void ClickSmallToBig()
    {
        BigMap.SetActive(true);
        miniMapCamera.orthographicSize = 10f;
        SmallMap.SetActive(false);
    }

    public void MakeBigToSmall()
    {
        SmallMap.SetActive(true);
        miniMapCamera.orthographicSize = 5f;
        BigMap.SetActive(false);
    }
}
