using UnityEngine;

namespace LevelScreen
{
    public class ToolTip : MonoBehaviour
    {
        public string message;

        private void OnMouseEnter()
        {
            ToolTipManager.Instance.SetAndShowTooltip(message);
        }

        private void OnMouseExit()
        {
            ToolTipManager.Instance.HideTooltip();
        }
    }
}