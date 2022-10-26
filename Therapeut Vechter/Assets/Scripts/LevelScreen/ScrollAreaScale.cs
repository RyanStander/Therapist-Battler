using UnityEngine;

namespace LevelScreen
{
    public class ScrollAreaScale : MonoBehaviour
    {
        private GameObject[] blockPrefabs;
        private int distanceBetweenObjects;
        private int areaWidth;
        void Start()
        {
            distanceBetweenObjects = GameObject.Find("Spawning").GetComponent<SpawnLevels>().SpawnDistance;
            blockPrefabs = Resources.LoadAll<GameObject>("Prefabs");
            for (var i = 0; i < blockPrefabs.Length; i++)
            {
                areaWidth += distanceBetweenObjects;
            }
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(areaWidth,1080);
        }

    }
}
