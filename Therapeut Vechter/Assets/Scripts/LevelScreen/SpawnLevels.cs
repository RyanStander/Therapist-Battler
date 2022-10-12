using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace LevelScreen
{
    public class SpawnLevels : MonoBehaviour
    {
        [SerializeField] private GameObject[] blockPrefabs;
        [SerializeField] private int spawnDistance = 50;
        [SerializeField] private int startDistance = 25;
        private Vector3 forward;
        public LineRenderer lineRend;
        public Transform[] levelPrefabPosition;

        private void Start()
        {
            lineRend.startWidth = 2;
            blockPrefabs = Resources.LoadAll<GameObject>("Prefabs");
            for (var i = 0; i < blockPrefabs.Length; i++)
            {
                var levelPrefab = Instantiate(blockPrefabs[i], new Vector3(startDistance + i * spawnDistance, 0, 0),
                    Quaternion.identity, transform);
            }
        }

        private void Update()
        {
            levelPrefabPosition = transform.Cast<Transform>().ToArray();
            Debug.Log(levelPrefabPosition);
            // Set the position count of the line renderer to certain amount
            lineRend.positionCount = levelPrefabPosition.Length;

            for (var i = 0; i < levelPrefabPosition.Length; i++)
            {
                lineRend.SetPosition(i,
                    new Vector3(levelPrefabPosition[i].transform.localPosition.x,
                        levelPrefabPosition[i].transform.localPosition.y, 0));
            }
        }
    }
}