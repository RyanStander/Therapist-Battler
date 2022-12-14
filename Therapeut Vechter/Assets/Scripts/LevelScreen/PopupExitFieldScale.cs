using UnityEngine;

namespace LevelScreen
{
    public class PopupExitFieldScale : MonoBehaviour
    {
        private Transform screenSize;
        void Awake()
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width,Screen.height);
        }
    }
}
