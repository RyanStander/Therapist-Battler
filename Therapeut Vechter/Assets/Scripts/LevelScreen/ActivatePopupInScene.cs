using UnityEngine;

namespace LevelScreen
{
    public class ActivatePopupInScene : MonoBehaviour
    {
        [SerializeField] private GameObject targetObject;
        [SerializeField] private GameObject parentObj;
        public float scrollAreaPosX;
        private float thisAreaPosX;
        public void CheckScene()
        {
            targetObject.SetActive(true);
            targetObject.transform.parent = parentObj.transform.parent;
        }

        public void CenterInScreen()
        {
            Debug.Log("working");
            scrollAreaPosX = GameObject.Find("Scrollable area").transform.localPosition.x;
            Debug.Log(scrollAreaPosX);
            thisAreaPosX = transform.parent.localPosition.x;
            Debug.Log(thisAreaPosX);
            //GameObject.Find("Scrollable area").transform.localPosition.x = thisAreaPosX;
        }
    }
}
