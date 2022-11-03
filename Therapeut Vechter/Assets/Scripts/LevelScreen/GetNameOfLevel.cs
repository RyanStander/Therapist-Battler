using TMPro;
using UnityEngine;


namespace LevelScreen
{
    public class GetNameOfLevel : MonoBehaviour
    {
        [SerializeField] private TMP_Text levelName;

        private string levelString;

        //put name of level on the popup, gets name from the leveldata script
        private void Start()
        {
            levelString = gameObject.GetComponent<LevelScript>().Name;
            levelName.text = levelString;
        }
    }
}