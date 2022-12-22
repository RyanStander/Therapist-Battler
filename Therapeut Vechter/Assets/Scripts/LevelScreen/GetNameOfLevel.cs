using TMPro;
using UnityEngine;


namespace LevelScreen
{
    public class GetNameOfLevel : MonoBehaviour
    {
        [SerializeField] private TMP_Text levelName;

        [SerializeField]
        private string popUpCartTitle;

        //put name of level on the popup, gets name from the leveldata script
        private void Start()
        {
            levelName.text = popUpCartTitle;
        }
    }
}