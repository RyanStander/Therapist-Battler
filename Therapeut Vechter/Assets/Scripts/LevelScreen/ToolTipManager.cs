using TMPro;
using UnityEngine;

namespace LevelScreen
{
    public class ToolTipManager : MonoBehaviour
    {
        public static ToolTipManager Instance;
        [SerializeField] private TextMeshProUGUI TextComponent;
        [SerializeField] private Canvas parentCanvas;

        private void Awake()
        {
            CheckObjectInScene();
        }

        void Start()
        {
            Cursor.visible = true;
            gameObject.SetActive(false);
        }

        void Update()
        {
            //transform.localPosition = Input.mousePosition;

            Vector2 movePos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                parentCanvas.transform as RectTransform,
                Input.mousePosition, parentCanvas.worldCamera,
                out movePos);
            transform.position = parentCanvas.transform.TransformPoint(movePos);
        }

        public void SetAndShowTooltip(string message)
        {
            gameObject.SetActive(true);
            TextComponent.text = message;
        }

        public void HideTooltip()
        {
            gameObject.SetActive(false);
            TextComponent.text = string.Empty;
        }

        private void CheckObjectInScene()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
    }
}