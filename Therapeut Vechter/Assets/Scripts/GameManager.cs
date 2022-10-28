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

    [SerializeField] private AudioSource dialogueAudioSource;

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
        }else if (gameEventDataHolder.gameEvents[gameEventsIndex] is DialogueData dialogueEvent)
        {
            ManageDialogueEvent(dialogueEvent);
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
            dialogueAudioSource.PlayOneShot(puzzleEvent.exerciseData[eventExerciseDataIndex].VoiceLineToPlay);
            exerciseImage.sprite = puzzleEvent.exerciseData[eventExerciseDataIndex].SpriteToShow;
        }

        var score = poseMatchCheck.PoseScoring(puzzleEvent.exerciseData[eventExerciseDataIndex].ExerciseToPerform
            .poseDatas[poseDataProgress]);

        //if it returns -1 it means the player did not achieve a good pose
        if (score == -1)
            return;

        totalScore += score;
        poseDataProgress++;
    }

    private void ManageDialogueEvent(DialogueData dialogueEvent)
    {
        if (!dialogueAudioSource.isPlaying&& !hasPlayedAudio)
        {
            dialogueAudioSource.clip = dialogueEvent.DialogueClip;
            dialogueAudioSource.Play();
            hasPlayedAudio = true;
        }else if (!dialogueAudioSource.isPlaying&&hasPlayedAudio)
        {
            hasPlayedAudio = false;
            gameEventsIndex++; 
        }
    }
    
    private void SlowScoreIncreaseOverTime()
    {
        currentDisplayScore = Mathf.Lerp(currentDisplayScore, totalScore * scoreModifier, scoreUpdateSpeed);

        scoreText.text = Mathf.Floor(currentDisplayScore).ToString();
    }
}
