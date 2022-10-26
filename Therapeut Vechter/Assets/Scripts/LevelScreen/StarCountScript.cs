using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace LevelScreen
{
    public class StarCountScript : MonoBehaviour
    {
        [SerializeField] private TMP_Text starCountText;
        private string starCountString;
        private int maxStarCount;
        public int starsInScene;
        private bool starCounting;
        private GameObject[] gameObjects;

        void Start()
        {
            StartCoroutine(WaitAndPrint());
        }

        void Update()
        {
            starCountString = starsInScene.ToString();
            starCountString += "/" + maxStarCount;
            starCountText.text = starCountString;
        }

        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator WaitAndPrint()
        {
            yield return new WaitForSeconds(0.01f);
            foreach (GameObject levelObj in GameObject.FindGameObjectsWithTag("LevelObject"))
            {
                maxStarCount += 3;
            }

            foreach (GameObject starObj in GameObject.FindGameObjectsWithTag("Star"))
            {
                starsInScene += 1;
            }
        }
    }
}