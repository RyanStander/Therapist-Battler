using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Exercises;
using GameEvents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameEventDataHolder gameEventDataHolder;
    [SerializeField] private PoseMatchCheck poseMatchCheck;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Range(0, 1)] [SerializeField] private float scoreUpdateSpeed = 0.5f;
    [SerializeField] private float scoreModifier = 100;

    [Header("Game event objects")] [SerializeField]
    private Image exerciseImage;

    [SerializeField] private AudioSource exerciseAudioSource;

    private List<BaseGameEvent> gameEvents = new List<BaseGameEvent>();
    private Queue<GameEventData> gameEventDatas = new Queue<GameEventData>();
    private BaseGameEvent currentGameEvent;
    private GameEventData currentgameGameEventData;

    private int poseDataProgress;

    private float totalScore;
    private float currentDisplayScore;

    private bool levelCleared;
    private bool exerciseComplete;

    private void Awake()
    {
        //Sets the starting values for the game
        InitialiseGame();
        scoreText.text = 0.ToString();
    }

    private void FixedUpdate()
    {
        RunLevel();

        SlowScoreIncreaseOverTime();
    }

    private void InitialiseGame()
    {
        //Enques all of the game events into a queue
        foreach (var gameEvent in gameEventDataHolder.gameEvents)
        {
            gameEvents.Enqueue(gameEvent);
        }

        //Starts the first event
        AdvanceToNextEvent();
    }

    //Manages the functionality of the level
    private void RunLevel()
    {
        //Do not proceed if the level has been finished
        //TODO: Implement functionality for finishing a level
        if (levelCleared)
            return;
        
        if (gameEventDatas.Count == 0 && (currentgameGameEventData.ExerciseToPerform == null ||
            currentgameGameEventData.ExerciseToPerform.poseDatas.Count == poseDataProgress))
        {
            PerformNextActionInEvent();
        }

        CheckExercise();
    }
    
    private void PerformNextActionInEvent()
    {
        Debug.Log("Moving to next action");
        //go on to next action

        if (currentGameEvent is EnvironmentPuzzleData environmentPuzzleData)
        {
            foreach (var poseDataSet in environmentPuzzleData.exerciseData)
            {
                gameEventDatas.Enqueue(poseDataSet);
            }
        }

        AdvanceToNextEvent();
    }

    private void AdvanceToNextEvent()
    {
        if (gameEvents.Count > 0)
        {
            currentGameEvent = gameEvents.Dequeue();
        }
    }

    private void CheckExercise()
    {
        if (currentgameGameEventData == null || currentgameGameEventData.ExerciseToPerform == null ||
            currentgameGameEventData.ExerciseToPerform.poseDatas.Count - 1 < poseDataProgress)
        {
            poseDataProgress = 0;
            AdvanceExercise();
            return;
        }

        var score = poseMatchCheck.PoseScoring(currentgameGameEventData.ExerciseToPerform.poseDatas[poseDataProgress]);

        if (score == -1)
            return;

        totalScore += score;
        scoreText.text = totalScore.ToString(CultureInfo.CurrentCulture);
        poseDataProgress++;

        if (gameEvents.Count == 0 && gameEventDatas.Count==0  &&
            (currentgameGameEventData.ExerciseToPerform.poseDatas.Count == poseDataProgress&&currentgameGameEventData.ExerciseToPerform.poseDatas.Count!=0)&& exerciseComplete)
        {
            Debug.Log("Level cleared");
            levelCleared = true;
        }
    }
    
    private void AdvanceExercise()
    {
        Debug.Log("Advancing exercise");
        exerciseComplete = false;
        currentgameGameEventData = gameEventDatas.Dequeue();

        if (currentgameGameEventData != null)
        {
            //Play sound
            exerciseAudioSource.clip = currentgameGameEventData.VoiceLineToPlay;
            exerciseAudioSource.Play();

            //display visuals
            exerciseImage.sprite = currentgameGameEventData.SpriteToShow;
        }
    }

    private void SlowScoreIncreaseOverTime()
    {
        currentDisplayScore = Mathf.Lerp(currentDisplayScore, totalScore * scoreModifier, scoreUpdateSpeed);

        scoreText.text = Mathf.Floor(currentDisplayScore).ToString();
    }
}