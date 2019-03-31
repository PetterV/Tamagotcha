using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSortingHandler : MonoBehaviour
{

    Canvas canvasRenderer;
    // Start is called before the first frame update
    void Start()
    {
        canvasRenderer = gameObject.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        float sortingOrderFloat = transform.position.z * - 100f;
        canvasRenderer.sortingOrder = (int)Mathf.Round(sortingOrderFloat);
    }
}
