using System;
using System.Collections;
using System.Collections.Generic;
using Exercises;
using GameEvents;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameEventDataHolder gameEventDataHolder;
    [SerializeField] private PoseMatchCheck poseMatchCheck;
    private Queue<BaseGameEvent> gameEvents=new Queue<BaseGameEvent>();
    private Queue<PoseDataSet> poseDataSets = new Queue<PoseDataSet>();
    private BaseGameEvent currentGameEvent;
    private PoseDataSet currentPoseDataSet;

    private int poseDataProgress;
    private void Awake()
    {
        InitialiseGame();
    }

    private void FixedUpdate()
    {
        RunLevel();
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
        if (poseDataSets.Count==0)
        {
            PerformNextActionInEvent();
        }
        
        CheckExercise();
    }
    
    private void AdvanceLevel()
    {
        if (gameEvents.Count>0)
        {
            Debug.Log("Advancing level");
            currentGameEvent = gameEvents.Dequeue();
        }else
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
        if (currentPoseDataSet==null||currentPoseDataSet.poseDatas.Count-1<poseDataProgress)
        {
            poseDataProgress = 0;
            AdvanceExercise();
        }
        else if (poseMatchCheck.PoseMatches(currentPoseDataSet.poseDatas[poseDataProgress]))
        {
            Debug.Log("Good job!");
            poseDataProgress++;
        }
    }
}

