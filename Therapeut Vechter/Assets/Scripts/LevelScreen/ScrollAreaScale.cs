using System;
using System.Collections;
using UnityEngine;

namespace LevelScreen
{
    public class ScrollAreaScale : MonoBehaviour
    {
        [SerializeField] private GameObjectSpawn gameObjectSpawn;

        //create size x-axis for scroll area
        private int distanceBetweenObjects;
        private int areaWidth;
        private int objectSpawned;
        private bool distanceCreated;

        //snapping to pop up
        private Vector3 positionA;
        private Vector3 positionB;
        private bool moveCanvas;
        private const float LerpSpeed = 0.1f;

        private void OnValidate()
        {
            if (gameObjectSpawn == null)
            {
                gameObjectSpawn = GameObject.Find("LevelSpawner").GetComponent<GameObjectSpawn>();
            }
        }

        private void Start()
        {
            distanceBetweenObjects = gameObjectSpawn.SpawnDistance;
        }

        // snapping scroll to selected pop up
        public void SetSnapPosition(float areaPosX)
        {
            positionB = new Vector3(-areaPosX, 0, 0);
            positionA = new Vector3(transform.localPosition.x, 0, 0);
            moveCanvas = true;
            StartCoroutine(StopSnapAfterTime(0.5f));
        }

        private void Update()
        {
            GenerateDistance();
            //make the map slide to the snap point
            if (!moveCanvas) 
                return;
            //positionA = Vector3.Lerp(transform.localPosition, positionB, LerpSpeed);
            var xPos = Mathf.Lerp(transform.localPosition.x, positionB.x, LerpSpeed);
            positionA.x = xPos;
            transform.localPosition = positionA;
        }

        private void GenerateDistance()
        {
            if (distanceCreated)
                return;

            objectSpawned = gameObjectSpawn.ObjectsSpawned;
            for (var i = 0; i < objectSpawned; i++)
            {
                areaWidth += distanceBetweenObjects;
            }

            distanceCreated = true;
            var a = gameObject.GetComponent<RectTransform>();
            a.sizeDelta = new Vector2(areaWidth, a.sizeDelta.y);
        }

        private IEnumerator StopSnapAfterTime(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);

            moveCanvas = false;
        }
    }
}