using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MagicNames
{
    fireCircle = 0
}

public class PlayerMagicManager : MonoBehaviour
{
    public GameObject[] playerMagics;

    private static PlayerMagicManager instance = null;
    public static PlayerMagicManager Instance
    {
        get
        {
            if(instance != null)
            {
                return instance;
            }
            return null;
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //if (PlayerMagics.Keys.Count> 0)
        //{
        //    foreach(GameObject objs in PlayerMagics.Values)
        //    {
        //        objs.SetActive(true);
        //        objs.SetActive(false);
        //    }
        //}

        for(int i=0; i < playerMagics.Length; i++)
        {
            playerMagics[i].SetActive(true);
            playerMagics[i].SetActive(false);
        }
    }

    public void OnPlayerMagic(int _magicIndex)
    {
        //GameObject magic = null;
        //if (PlayerMagics.ContainsKey(_magicName))
        //{
        //    PlayerMagics.TryGetValue(_magicName, out magic);
        //    magic.SetActive(true);
        //}
        playerMagics[_magicIndex].SetActive(true);
    }

    public void OffPlayerMagic(int _magicIndex)
    {
        playerMagics[_magicIndex].SetActive(false);
    }
}
