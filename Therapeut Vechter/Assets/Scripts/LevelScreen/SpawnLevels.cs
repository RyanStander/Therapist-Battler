using System.Linq;
using UnityEngine;

namespace LevelScreen
{
    public class SpawnLevels : MonoBehaviour
    {
        [SerializeField] private GameObject[] blockPrefabs;
        [SerializeField] private int spawnDistance = 50;
        [SerializeField] private int startDistance = 25;
        [SerializeField] private int lineWidth;
        [SerializeField] private LineRenderer lineRenderComplete;
        private bool lineCompleteDrawn;
        private int lastCompleteInt;
        [SerializeField] private LineRenderer lineRenderUnComplete;
        private int lineUnCompleteInt;
        [SerializeField] private LineRenderer lineRenderGradient;
        private int lineGradientInt;
        private bool lineGradientDrawn;
        private bool allLinesDrawn;
        public Transform[] levelPrefabPosition;

        private void Start()
        {
            //set line render width
            lineRenderComplete.startWidth = lineWidth;
            lineRenderUnComplete.startWidth = lineWidth;
            lineRenderGradient.startWidth = lineWidth;
            //spawn level objects in
            blockPrefabs = Resources.LoadAll<GameObject>("Prefabs");
            for (var i = 0; i < blockPrefabs.Length; i++)
            {
                var levelPrefab = Instantiate(blockPrefabs[i], new Vector3(startDistance + i * spawnDistance, 0, 0),
                    Quaternion.identity, transform);
            }
        }

        //line renderer functionality
        private void Update()
        {
            //get all children in spawning object to draw line between
            levelPrefabPosition = transform.Cast<Transform>().ToArray();
            //line drawing
            if (allLinesDrawn == false)
            {
                for (var i = 0; i < levelPrefabPosition.Length; i++)
                {
                    //draw line between finished levels
                    if (levelPrefabPosition[i].GetComponent<LevelFunctionality>().IsLevelFinished &&
                        lineCompleteDrawn == false)
                    {
                        lineRenderComplete.positionCount += 1;
                        lineRenderComplete.SetPosition(i,
                            new Vector3(levelPrefabPosition[i].transform.localPosition.x,
                                levelPrefabPosition[i].transform.localPosition.y, 0));
                        //get the last item location to draw gradient line from
                        lastCompleteInt += 1;
                    }

                    //draw line between unfinished levels
                    if (levelPrefabPosition[i].GetComponent<LevelFunctionality>().IsLevelFinished == false)
                    {
                        lineCompleteDrawn = true;
                        lineRenderUnComplete.positionCount += 1;
                        lineRenderUnComplete.SetPosition(lineUnCompleteInt,
                            new Vector3(levelPrefabPosition[i].transform.localPosition.x,
                                levelPrefabPosition[i].transform.localPosition.y, 0));
                        lineUnCompleteInt += 1;
                    }

                    //draw gradient line to current level
                    if (lineGradientDrawn == false && lineCompleteDrawn == true)
                    {
                        lineRenderGradient.positionCount = 2;
                        lineRenderGradient.SetPosition(0,
                            new Vector3(levelPrefabPosition[lastCompleteInt - 1].transform.localPosition.x,
                                levelPrefabPosition[lastCompleteInt - 1].transform.localPosition.y, 0));
                        
                        lineRenderGradient.SetPosition(1,
                            new Vector3(levelPrefabPosition[lastCompleteInt].transform.localPosition.x,
                                levelPrefabPosition[lastCompleteInt].transform.localPosition.y, 0));
                        
                        lineGradientDrawn = true;
                    }

                    if (i <= levelPrefabPosition.Length)
                    {
                        allLinesDrawn = true;
                    }
                }
            }
        }
    }
}