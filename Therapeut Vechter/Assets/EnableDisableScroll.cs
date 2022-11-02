using UnityEngine;
using UnityEngine.UI;

public class EnableDisableScroll : MonoBehaviour
{
    public ScrollRect scrollImageObj;
    public GameObject test;


    void Start()
    {
        scrollImageObj = transform.GetComponent<ScrollRect>();
    }
    
    void Update()
    {
        test = GameObject.Find("LevelOptionParent");
        if (test.activeInHierarchy == true)
        {
            scrollImageObj.enabled = false;
        }

        if (test == false)
        {
            scrollImageObj.enabled = true;
        }
    }
}
