using System.Collections;
using TMPro;
using UnityEngine;

namespace LevelScreen
{
    public class StarCountScript : MonoBehaviour
    {
        [SerializeField] private TMP_Text starCountText;
        private string starCountString;
        private int maxStarCount;
        public int StarsInScene;
        private GameObject[] gameObjects;

        void Start()
        {
            //start counting after all objects have spawned in
            StartCoroutine(WaitAndPrint());
        }

        void Update()
        {
            //change text in the gem to display current amount
            starCountString = StarsInScene.ToString();
            starCountString += "/" + maxStarCount;
            starCountText.text = starCountString;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator WaitAndPrint()
        {
            //add 3 stars per level counted and a 1 total stars unlocked per object found with a star tag
            yield return new WaitForSeconds(0.01f);
            foreach (GameObject levelObj in GameObject.FindGameObjectsWithTag("LevelObject"))
            {
                maxStarCount += 3;
            }

            foreach (GameObject starObj in GameObject.FindGameObjectsWithTag("Star"))
            {
                StarsInScene += 1;
            }
        }
    }
}