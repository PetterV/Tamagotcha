using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteHandler : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float sortingOrderFloat = transform.position.z * -100;
        spriteRenderer.sortingOrder = (int)Mathf.Round(sortingOrderFloat);
    }
}
