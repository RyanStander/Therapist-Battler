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
    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private Slider enemyHealthBar;//TODO: separate to own script functionality

    [SerializeField] private float comboDuration=3;
    
    private bool hasSwappedMusicAudioSource;
    
    private int gameEventDatasIndex;
    private BaseGameEvent currentGameEvent;
    private GameEventData currentgameGameEventData;

    private int poseDataProgress;

    private float totalScore;
    private float currentDisplayScore;

    private bool levelCleared;
    private bool exerciseComplete;


    //TODO: Move around for organising
    public int eventExerciseDataIndex;
    public int poseDataIndex;

    private bool hasPlayedAudio;

    public int playerAttackIndex;
    private float enemyHealth;
    private float enemyCurrentDisplayHealth;
    private bool hasSetupEnemyFirstTime;

    private float timeStamp;
    public int comboCount;
    private float comboCountDamageModifier;
    //The damage that the player will deal to the enemy
    private float playerDamage;

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

        if (gameEventDataHolder.gameEvents[gameEventsIndex].OverrideCurrentlyPlayingMusic&&!hasSwappedMusicAudioSource)
        {
            musicAudioSource.Stop();
            musicAudioSource.clip = gameEventDataHolder.gameEvents[gameEventsIndex].OverrideMusic;
            musicAudioSource.Play();
            hasSwappedMusicAudioSource = true;
        }
        
        if (gameEventDataHolder.gameEvents[gameEventsIndex] is EnvironmentPuzzleData puzzleEvent)
        {
            ManagePuzzleEvent(puzzleEvent);
        }else if (gameEventDataHolder.gameEvents[gameEventsIndex] is DialogueData dialogueEvent)
        {
            ManageDialogueEvent(dialogueEvent);
        }else if (gameEventDataHolder.gameEvents[gameEventsIndex] is FightingData fightingData)
        {
            ManageFightingEvent(fightingData);    
        }
    }

    private void ManageFightingEvent(FightingData fightingEvent)
    {
        //if the enemy has just appeared
        if (!hasSetupEnemyFirstTime)
        {
            hasSetupEnemyFirstTime = true;
            //set background image
            enemyHealth = fightingEvent.enemyHealth;
            enemyHealthBar.maxValue = enemyHealth;
            enemyHealthBar.value = enemyHealth;
            enemyCurrentDisplayHealth = enemyHealth;
        }
        
        //if enemy dies
        if (enemyHealth<1)
        {
            hasSetupEnemyFirstTime = false;
            gameEventsIndex++;
            hasPlayedAudio = false;
            eventExerciseDataIndex = 0;
            poseDataIndex = 0;
            playerAttackIndex = 0;
            return;
        }

        if (!hasPlayedAudio && eventExerciseDataIndex==0)
        {
            hasPlayedAudio = true;
            dialogueAudioSource.PlayOneShot(fightingEvent.playerAttackSequence[playerAttackIndex].exerciseName);
        }

        if (fightingEvent.playerAttackSequence[playerAttackIndex].playerAttack[eventExerciseDataIndex].poseDatas.Count <= poseDataProgress)
        {
            poseDataProgress = 0;
            eventExerciseDataIndex++;
            timeStamp = Time.time + comboDuration;
            comboCount++;
            enemyHealth -= playerDamage;

            if (fightingEvent.playerAttackSequence[playerAttackIndex].playerAttack.Length<= eventExerciseDataIndex)
            {
                playerAttackIndex++;
                eventExerciseDataIndex = 0;
                
                
                
                //TODO: handle player damage here

                //We reset the attack index so that it starts the first attack again
                if (fightingEvent.playerAttackSequence.Length<=playerAttackIndex)
                {
                    playerAttackIndex = 0;
                }

                dialogueAudioSource.PlayOneShot(fightingEvent.playerAttackSequence[playerAttackIndex].exerciseName);
            }
        }

        //have a combo timer running, depending on how many combos they get, they get higher damage
        if (timeStamp<=Time.time)
        {
            enemyHealth -= comboCount * comboCountDamageModifier;
            comboCount = 0;
        }
        
        var score = poseMatchCheck.PoseScoring(fightingEvent.playerAttackSequence[playerAttackIndex].playerAttack[eventExerciseDataIndex].poseDatas[poseDataProgress]);

        //if it returns -1 it means the player did not achieve a good pose
        if (score == -1)
            return;

        playerDamage += score;
        totalScore += score;
        poseDataProgress++;
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
        //TODO: make it so combo timer is updated here
        
        currentDisplayScore = Mathf.Lerp(currentDisplayScore, totalScore * scoreModifier, scoreUpdateSpeed);

        scoreText.text = Mathf.Floor(currentDisplayScore).ToString();

        enemyCurrentDisplayHealth = Mathf.Lerp(enemyCurrentDisplayHealth, enemyHealth, scoreUpdateSpeed);
        
        enemyHealthBar.value = enemyCurrentDisplayHealth;
    }
}
