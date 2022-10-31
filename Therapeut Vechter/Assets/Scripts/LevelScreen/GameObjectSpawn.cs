using UnityEngine;

namespace LevelScreen
{
    public class GameObjectSpawn : MonoBehaviour
    {
        public GameObject LevelPrefab;
        public LevelData[] LevelData;
        public int SpawnDistance;
        public int ObjectsSpawned;

        private void Start()
        {
            for (var i = 0; i < LevelData.Length; i++)
            {
                var newLevel  = Instantiate(LevelPrefab, new Vector3(150+ i*SpawnDistance,0,0),Quaternion.identity,transform);
                newLevel.GetComponent<LevelScript>().LoadLevelData(LevelData[i]);
                ObjectsSpawned += 1;
            }
        }
    }
}
