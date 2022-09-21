using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class ShowFPS : MonoBehaviour {
    public Text fpsText;
    public float deltaTime;
 
    void Update () {
        deltaTime += (Time.deltaTime - deltaTime) * 0.05f;
        float fps = 1.0f / deltaTime;
        fpsText.text = "FPS: " + Mathf.Ceil(fps).ToString("F1");
    }
}