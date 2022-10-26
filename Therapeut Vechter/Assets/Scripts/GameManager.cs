using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Exercises;
using GameEvents;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameEventDataHolder gameEventDataHolder;
    [SerializeField] private PoseMatchCheck poseMatchCheck;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Range(0,1)][SerializeField] private float scoreUpdateSpeed=0.5f;
    [SerializeField] private float scoreModifier = 100;
    
    private Queue<BaseGameEvent> gameEvents = new Queue<BaseGameEvent>();
    private Queue<PoseDataSet> poseDataSets = new Queue<PoseDataSet>();
    private BaseGameEvent currentGameEvent;
    private PoseDataSet currentPoseDataSet;

    private int poseDataProgress;

    private float totalScore;
    private float currentDisplayScore;

    private void Awake()
    {
        InitialiseGame();
        scoreText.text = 0.ToString();
    }

    private void FixedUpdate()
    {
        RunLevel();

        currentDisplayScore = Mathf.Lerp(currentDisplayScore, totalScore * scoreModifier, scoreUpdateSpeed);

        scoreText.text = Mathf.Floor(currentDisplayScore).ToString();
    }

    private void InitialiseGame()
    {
        foreach (var gameEvent in gameEventDataHolder.gameEvents)
        {
            gameEvents.Enqueue(gameEvent);
        }

        AdvanceLevel();
    }

    private void RunLevel()
    {
        if (poseDataSets.Count == 0)
        {
            PerformNextActionInEvent();
        }

        CheckExercise();
    }

    private void AdvanceLevel()
    {
        if (gameEvents.Count > 0)
        {
            Debug.Log("Advancing level");
            currentGameEvent = gameEvents.Dequeue();
        }
        else
            Debug.Log("Level cleared");
    }

    private void PerformNextActionInEvent()
    {
        Debug.Log("Moving to next action");
        //go on to next action

        if (currentGameEvent is EnvironmentPuzzleData environmentPuzzleData)
        {
            foreach (var poseDataSet in environmentPuzzleData.exerciseToPerform)
            {
                poseDataSets.Enqueue(poseDataSet);
            }
        }

        AdvanceLevel();
    }

    private void AdvanceExercise()
    {
        currentPoseDataSet = poseDataSets.Dequeue();
    }

    private void CheckExercise()
    {
        if (currentPoseDataSet == null || currentPoseDataSet.poseDatas.Count - 1 < poseDataProgress)
        {
            poseDataProgress = 0;
            AdvanceExercise();
            return;
        }

        var score = poseMatchCheck.PoseScoring(currentPoseDataSet.poseDatas[poseDataProgress]);
        if (score == -1)
            return;
        
        Debug.Log(score);
        
        totalScore += score;
        scoreText.text = totalScore.ToString(CultureInfo.CurrentCulture);
        poseDataProgress++;
    }
}