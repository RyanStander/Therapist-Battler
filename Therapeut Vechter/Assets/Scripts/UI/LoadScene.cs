using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] private string sceneName;

        public void LoadSceneByNane()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}