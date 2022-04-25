using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class organize : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.SetParent(GameObject.Find("DataManager").transform);
    }

}
