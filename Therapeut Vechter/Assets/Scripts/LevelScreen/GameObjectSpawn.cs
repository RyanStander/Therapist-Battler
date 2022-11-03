using System;
using System.Linq;
using UnityEngine;

namespace LevelScreen
{
    public class GameObjectSpawn : MonoBehaviour
    {
        public GameObject LevelPrefab;
        public LevelData[] LevelData;
        public int SpawnDistance;
        public int ObjectsSpawned;
        private Transform[] objList;
        //get the last 2 objects of the spawned in levels
        private int lastIndexInt;
        public GameObject lastArrayObj;
        private int secondLastIndexInt;
        public GameObject secondLastArrayObj;

        private void Start()
        {
            for (var i = 0; i < LevelData.Length; i++)
            {
                var newLevel  = Instantiate(LevelPrefab, new Vector3(150+ i*SpawnDistance,0,0),Quaternion.identity,transform);
                newLevel.GetComponent<LevelScript>().LoadLevelData(LevelData[i]);
                ObjectsSpawned += 1;
            }
            //linking the last levels spawned in to a public gameobject
            objList = transform.Cast<Transform>().ToArray();
            lastIndexInt =  objList.Length - 1;
            lastArrayObj = objList[lastIndexInt].gameObject;
            lastArrayObj.transform.Find("LevelOptionParent").localPosition = new Vector3(-250,50,0);
            lastArrayObj.transform.Find("LevelOptionParent/PointArrow").localPosition = new Vector3(160,-50,0);
            lastArrayObj.transform.Find("LevelOptionParent/PointArrow").rotation =Quaternion.Euler(0, 0, 90);
            //second last object linked
            secondLastIndexInt =  objList.Length - 2;
            secondLastArrayObj = objList[secondLastIndexInt].gameObject;
            secondLastArrayObj.transform.Find("LevelOptionParent").localPosition = new Vector3(-250,50,0);
            secondLastArrayObj.transform.Find("LevelOptionParent/PointArrow").localPosition = new Vector3(160,-50,0);
            secondLastArrayObj.transform.Find("LevelOptionParent/PointArrow").rotation =Quaternion.Euler(0, 0, 90);
        }
    }
}
