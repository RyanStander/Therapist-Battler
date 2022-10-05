using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLevels : MonoBehaviour
{
    public GameObject[] blockPrefabs;

    void Start()
    {
        blockPrefabs = Resources.LoadAll<GameObject>("Prefabs");

        Debug.Log(blockPrefabs);
        for (var i = 0; i < blockPrefabs.Length; i++)
        {
            Instantiate(blockPrefabs[i], new Vector3(i * 50.0f, 100, 0), Quaternion.identity, this.transform);
        }
    }
}
