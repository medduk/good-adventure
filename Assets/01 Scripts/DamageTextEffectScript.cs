using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTextEffectScript : MonoBehaviour
{
    MeshRenderer meshRenderer;

    [SerializeField] int sortingOrder;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void Start()
    {
        meshRenderer.sortingLayerName = "Arrow";
        meshRenderer.sortingOrder = this.sortingOrder;
    }
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime);
    }
}
