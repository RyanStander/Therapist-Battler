using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLevels : MonoBehaviour
{
    [SerializeField] private GameObject[] blockPrefabs;
    [SerializeField] private int spawnDistance = 50;
    [SerializeField] private int startDistance = 25;

    void Start()
    {
        blockPrefabs = Resources.LoadAll<GameObject>("Prefabs");
        for (var i = 0; i < blockPrefabs.Length; i++)
        {
            Instantiate(blockPrefabs[i], new Vector3(startDistance + i * spawnDistance, 0, 0), Quaternion.identity, this.transform);
        }
    }
}
