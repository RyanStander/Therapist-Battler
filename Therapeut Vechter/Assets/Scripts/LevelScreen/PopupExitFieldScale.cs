using UnityEngine;

namespace LevelScreen
{
    public class PopupExitFieldScale : MonoBehaviour
    {
        private Transform screenSize;
        void Update()
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width,Screen.height);
        }
    }
}
