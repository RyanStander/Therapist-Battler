using System.Linq;
using UnityEngine;

namespace LevelScreen
{
    public class LineRenderBetweenLevels : MonoBehaviour
    {
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
        private Transform[] levelPrefabPosition;

        void Start()
        {
            //set line render width and sorting layer
            lineRenderComplete.startWidth = lineWidth;
            lineRenderComplete.sortingOrder = -1;
            lineRenderUnComplete.startWidth = lineWidth;
            lineRenderUnComplete.sortingOrder = -1;
            lineRenderGradient.startWidth = lineWidth;
            lineRenderGradient.sortingOrder = -1;
        }

        //line renderer functionality
        private void Update()
        {
            DrawLines();
        }

        private void DrawLines()
        {
            //line drawing
            if (allLinesDrawn == false)
            {
                //get all children in spawning object to draw line between
                levelPrefabPosition = transform.Cast<Transform>().ToArray();
                for (var i = 0; i < levelPrefabPosition.Length; i++)
                {
                    //draw line between finished levels
                    if (levelPrefabPosition[i].GetComponent<LevelScript>().LevelFinished &&
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
                    if (levelPrefabPosition[i].GetComponent<LevelScript>().LevelFinished == false)
                    {
                        lineCompleteDrawn = true;
                        lineRenderUnComplete.positionCount += 1;
                        lineRenderUnComplete.SetPosition(lineUnCompleteInt,
                            new Vector3(levelPrefabPosition[i].transform.localPosition.x,
                                levelPrefabPosition[i].transform.localPosition.y, 0));
                        lineUnCompleteInt += 1;
                    }

                    //draw gradient line to current level
                    if (lineGradientDrawn == false && lineCompleteDrawn)
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