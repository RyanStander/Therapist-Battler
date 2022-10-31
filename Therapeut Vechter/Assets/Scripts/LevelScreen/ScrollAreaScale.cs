using UnityEngine;
namespace LevelScreen
{
    public class ScrollAreaScale : MonoBehaviour
    {
        private int distanceBetweenObjects;
        private int areaWidth;
        private int objectSpawned;
        void Start()
        {
            distanceBetweenObjects = GameObject.Find("LevelSpawner").GetComponent<GameObjectSpawn>().SpawnDistance;
            objectSpawned = GameObject.Find("LevelSpawner").GetComponent<GameObjectSpawn>().ObjectsSpawned;
            for (var i = 0; i < objectSpawned; i++)
            {
                areaWidth += distanceBetweenObjects;
            }
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(areaWidth,Screen.height);
        }
    }
}
