using System;
using UnityEngine;

namespace LevelScreen
{
    public class ScrollAreaScale : MonoBehaviour
    {
        //create size x-axis for scroll area
        private int distanceBetweenObjects;
        private int areaWidth;
        private int objectSpawned;

        //snapping to pop up
        private Vector3 positionA;
        private Vector3 positionB;
        private bool moveCanvas;
        private float lerpSpeed = 0.1f;

        void Start()
        {
            distanceBetweenObjects = GameObject.Find("LevelSpawner").GetComponent<GameObjectSpawn>().SpawnDistance;
            objectSpawned = GameObject.Find("LevelSpawner").GetComponent<GameObjectSpawn>().ObjectsSpawned;
            for (var i = 0; i < objectSpawned; i++)
            {
                areaWidth += distanceBetweenObjects;
            }

            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(areaWidth, Screen.height);
        }

        // snapping scroll to selected pop up
        public void SetSnapPosition(float areaPosX)
        {
            //gameObject.GetComponent<RectTransform>().localPosition = new Vector3(-areaPosX,0,0);
            positionB = new Vector3(-areaPosX, 0, 0);
            positionA = new Vector3(transform.localPosition.x, 0, 0);
            moveCanvas = true;
        }

        private void Update()
        {
            //make the map slide to the snap point
            //Debug.Log(moveCanvas);
            if (moveCanvas)
            {
                positionA = Vector3.Lerp(transform.localPosition, positionB, lerpSpeed);
                transform.localPosition = positionA;

                if (Math.Abs(positionA.x - positionB.x) < 0.1f)
                {
                    moveCanvas = false;
                }
            }

            //outer edge levels cause it to not be able to move to new position, this puts a buffer on it for the first and last levels
            if (transform.localPosition.x >= -960 ||
                transform.localPosition.x <= -areaWidth + distanceBetweenObjects * 3)
            {
                moveCanvas = false;
            }
        }
    }
}