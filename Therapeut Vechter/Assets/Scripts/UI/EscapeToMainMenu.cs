using UnityEngine;

namespace UI
{
    public class EscapeToMainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject menuParentGameObject;
        [SerializeField] private GameObject settingParentGameObject;
        [SerializeField] private GameObject roadmapUIGameObject;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) 
                return;
            menuParentGameObject.SetActive(true);
            roadmapUIGameObject.SetActive(false);
            settingParentGameObject.SetActive(false);
        }
    }
}