using TMPro;
using UnityEngine;


namespace LevelScreen
{
    public class GetNameOfLevel : MonoBehaviour
    {
        [SerializeField] private TMP_Text levelName;
        private string levelString;

        private void Start()
        {
            levelString = gameObject.GetComponent<LevelScript>().Name;
            levelName.text = levelString;
        }

    }
}
