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
    public int gameEventsIndex;

    [SerializeField] private PoseMatchCheck poseMatchCheck;
    [SerializeField] private TextMeshProUGUI scoreText;

    [Range(0, 1)] [SerializeField] private float scoreUpdateSpeed = 0.5f;
    [SerializeField] private float scoreModifier = 100;

    [Header("Game event objects")] [SerializeField]
    private Image exerciseImage;

    [SerializeField] private AudioSource exerciseAudioSource;

    private List<GameEventData> gameEventDatas = new List<GameEventData>();
    private int gameEventDatasIndex;
    private BaseGameEvent currentGameEvent;
    private GameEventData currentgameGameEventData;

    private int poseDataProgress;

    private float totalScore;
    private float currentDisplayScore;

    private bool levelCleared;
    private bool exerciseComplete;


    //TODO: Move around for organising
    private int eventExerciseDataIndex;
    private int poseDataIndex;

    private bool hasPlayedAudio;

    private void Awake()
    {
        //Sets the starting values for the game
        InitialiseGame();
    }

    private void FixedUpdate()
    {
        RunLevel();

        SlowScoreIncreaseOverTime();
    }

    private void InitialiseGame()
    {
        scoreText.text = 0.ToString();
    }

    //Manages the functionality of the level
    private void RunLevel()
    {
        if (gameEventsIndex>=gameEventDataHolder.gameEvents.Length)
            return;
        
        if (gameEventDataHolder.gameEvents[gameEventsIndex] is EnvironmentPuzzleData puzzleEvent)
        {
            ManagePuzzleEvent(puzzleEvent);
        }
    }

    private void ManagePuzzleEvent(EnvironmentPuzzleData puzzleEvent)
    {
        //if it reaches the end of the pose data list
        if (puzzleEvent.exerciseData[eventExerciseDataIndex].ExerciseToPerform.poseDatas.Count <= poseDataProgress)
        {
            eventExerciseDataIndex++;
            poseDataProgress = 0;
            hasPlayedAudio = false;
            
            if (puzzleEvent.exerciseData.Length == eventExerciseDataIndex)
            {
                eventExerciseDataIndex = 0;
                gameEventsIndex++;
                return;
            }

            return;
        }

        if (poseDataProgress==0 && !hasPlayedAudio)
        {
            hasPlayedAudio = true;
            exerciseAudioSource.PlayOneShot(puzzleEvent.exerciseData[eventExerciseDataIndex].VoiceLineToPlay);
            exerciseImage.sprite = puzzleEvent.exerciseData[eventExerciseDataIndex].SpriteToShow;
        }

        var score = poseMatchCheck.PoseScoring(puzzleEvent.exerciseData[eventExerciseDataIndex].ExerciseToPerform
            .poseDatas[poseDataProgress]);

        Debug.Log("Checking for: " + puzzleEvent.exerciseData[eventExerciseDataIndex].ExerciseToPerform.poseDatas[poseDataProgress].name);
        
        //if it returns -1 it means the player did not achieve a good pose
        if (score == -1)
            return;

        totalScore += score;
        poseDataProgress++;
    }


    private void SlowScoreIncreaseOverTime()
    {
        currentDisplayScore = Mathf.Lerp(currentDisplayScore, totalScore * scoreModifier, scoreUpdateSpeed);

        scoreText.text = Mathf.Floor(currentDisplayScore).ToString();
    }
}

/*private void PerformNextActionInEvent()
    {
        Debug.Log("Moving to next action");
        //go on to next action

        if (currentGameEvent is EnvironmentPuzzleData environmentPuzzleData)
        {
            foreach (var poseDataSet in environmentPuzzleData.exerciseData)
            {
                //gameEventDatas.Enqueue(poseDataSet);
            }
        }

        AdvanceToNextEvent();
    }

    private void AdvanceToNextEvent()
    {
        if (gameEvents.Count > 0)
        {
            //currentGameEvent = gameEvents.Dequeue();
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
        //currentgameGameEventData = gameEventDatas.Dequeue();

        if (currentgameGameEventData != null)
        {
            //Play sound
            exerciseAudioSource.clip = currentgameGameEventData.VoiceLineToPlay;
            exerciseAudioSource.Play();

            //display visuals
            exerciseImage.sprite = currentgameGameEventData.SpriteToShow;
        }
    }*/