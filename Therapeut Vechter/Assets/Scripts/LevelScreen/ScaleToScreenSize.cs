using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleToScreenSize : MonoBehaviour
{
    private Transform screenSize;
    void Update()
    {
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width,Screen.height);
    }
}
