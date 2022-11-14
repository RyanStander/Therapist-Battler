using UnityEngine;

namespace LevelScreen
{
    public class ActivatePopupInScene : MonoBehaviour
    {
        [SerializeField] private GameObject targetObject;
        [SerializeField] private GameObject parentObj;
        private float thisAreaPosX;
        private GameObject scrollObject;

        private void Start()
        {
            scrollObject = GameObject.Find("Scrollable area");
        }

        //set this object in the right spot in hierarchy for correct scene layers
        public void ActivatePopUp()
        {
            targetObject.SetActive(true);
            targetObject.transform.SetParent(parentObj.gameObject.transform.parent);
        }

        //send this objects position to ScrollAreaScale(script) to give the position to snap to
        public void CenterInScreen()
        {
            thisAreaPosX = transform.parent.localPosition.x;
            scrollObject.GetComponent<ScrollAreaScale>().SetSnapPosition(thisAreaPosX);
        }
    }
}