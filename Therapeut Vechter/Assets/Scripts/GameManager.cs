using Exercises;
using GameEvents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// Handles the game events and is used to play the level.
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Serialised Fields

    [Header("General")] [Tooltip("Holds the level that will be used")] [SerializeField]
    private GameEventDataHolder gameEventDataHolder;

    [Tooltip("script used to check if poses match")] [SerializeField]
    private PoseMatchCheck poseMatchCheck;


    [Header("UI")] [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Image exerciseImage;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Slider playerHealthBar;
    [SerializeField] private Slider enemyHealthBar; //TODO: separate to own script functionality
    [SerializeField] private Image enemyImage;

    [Header("Audio Source")] [SerializeField]
    private AudioSource dialogueAudioSource;

    [SerializeField] private AudioSource musicAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    [Header("Scoring")] [Range(0, 1)] [SerializeField]
    private float scoreUpdateSpeed = 0.5f;

    [SerializeField] private float scoreModifier = 100;
    [SerializeField] private float comboDuration = 3;

    [Tooltip("The damage modifier that is applied to how high the combo count is")] [SerializeField]
    private float comboCountDamageModifier; //TODO: if the combo damage can kill an enemy, it should do so immediately

    [Tooltip("The damage that the player will deal to the enemy")] [SerializeField]
    private float playerDamage;

    #endregion


    #region Private Private

    //the score that the player has achieved
    private float totalScore;

    //the last time stamp of a combo
    private float comboTimeStamp;

    //The total combo count
    private int comboCount;

    //The score that is currently displayed (the 2 values are lerped together to make a slow increase)
    private float currentDisplayScore;

    #region Audio Data

    //used to determine if music has been swapped, should only happen once
    private bool hasSwappedMusicAudioSource;

    //Used to play dialogue
    private bool hasPlayedDialogueAudio;

    #endregion

    #region Indexies

    //Primary index, used to determine what event will be played
    private int gameEventsIndex;

    //Used to determine which exercise is to be done
    private int eventExerciseDataIndex;

    //Used to determine how far the progress is in a pose data set
    private int poseDataIndex;

    //used specifically for fighting events to determine which attack the player does.
    private int playerAttackIndex;

    #endregion

    #region Fighting Data

    private float playerCurrentDisplayHealth;
    private float playerHealth = 100;
    private float enemyHealth;
    private float enemyCurrentDisplayHealth;
    private bool hasSetupEnemyFirstTime;

    #endregion

    #endregion

    #region Runtime

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

    #endregion


    private void InitialiseGame()
    {
        scoreText.text = 0.ToString();

        playerHealthBar.maxValue = playerHealth;
        playerHealthBar.value = playerHealth;

        enemyImage.gameObject.SetActive(false);
    }

    //Manages the functionality of the level
    private void RunLevel()
    {
        //Check if it has reached the
        if (gameEventsIndex >= gameEventDataHolder.gameEvents.Length)
            return;

        //Swaps song
        if (gameEventDataHolder.gameEvents[gameEventsIndex].OverrideCurrentlyPlayingMusic &&
            !hasSwappedMusicAudioSource)
        {
            musicAudioSource.Stop();
            musicAudioSource.clip = gameEventDataHolder.gameEvents[gameEventsIndex].OverrideMusic;
            musicAudioSource.Play();
            hasSwappedMusicAudioSource = true;
        }

        switch (gameEventDataHolder.gameEvents[gameEventsIndex])
        {
            case EnvironmentPuzzleData puzzleEvent:
                ManagePuzzleEvent(puzzleEvent);
                break;
            case DialogueData dialogueEvent:
                ManageDialogueEvent(dialogueEvent);
                break;
            case FightingData fightingData:
                ManageFightingEvent(fightingData);
                break;
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
            if (fightingEvent.enemySprite == null)
                enemyImage.gameObject.SetActive(false);
            else
            {
                enemyImage.gameObject.SetActive(true);
                enemyImage.sprite = fightingEvent.enemySprite;
            }
        }

        //if enemy dies
        if (enemyHealth < 1)
        {
            hasSetupEnemyFirstTime = false;
            gameEventsIndex++;
            hasSwappedMusicAudioSource = false;
            hasPlayedDialogueAudio = false;
            eventExerciseDataIndex = 0;
            poseDataIndex = 0;
            playerAttackIndex = 0;
            return;
        }

        if (!hasPlayedDialogueAudio && eventExerciseDataIndex == 0)
        {
            hasPlayedDialogueAudio = true;
            dialogueAudioSource.PlayOneShot(fightingEvent.playerAttackSequence[playerAttackIndex].exerciseName);
        }

        if (fightingEvent.playerAttackSequence[playerAttackIndex].playerAttack[eventExerciseDataIndex].poseDatas
                .Count <= poseDataIndex)
        {
            poseDataIndex = 0;
            eventExerciseDataIndex++;
            comboTimeStamp = Time.time + comboDuration;
            comboCount++;
            enemyHealth -= playerDamage;
            if (fightingEvent.enemyAttackedSounds.Length > 0)
                sfxAudioSource.PlayOneShot(
                    fightingEvent.enemyAttackedSounds[Random.Range(0, fightingEvent.enemyAttackedSounds.Length)]);

            if (fightingEvent.playerAttackSequence[playerAttackIndex].playerAttack.Length <= eventExerciseDataIndex)
            {
                playerAttackIndex++;
                eventExerciseDataIndex = 0;

                playerHealth -= fightingEvent.enemyDamage;
                if (fightingEvent.enemyAttackSounds.Length > 0)
                    sfxAudioSource.PlayOneShot(
                        fightingEvent.enemyAttackSounds[Random.Range(0, fightingEvent.enemyAttackSounds.Length)]);

                //We reset the attack index so that it starts the first attack again
                if (fightingEvent.playerAttackSequence.Length <= playerAttackIndex)
                {
                    playerAttackIndex = 0;
                }

                dialogueAudioSource.PlayOneShot(fightingEvent.playerAttackSequence[playerAttackIndex].exerciseName);
            }
        }

        //have a combo timer running, depending on how many combos they get, they get higher damage
        if (comboTimeStamp <= Time.time && comboCount > 0)
        {
            enemyHealth -= comboCount * comboCountDamageModifier;
            if (fightingEvent.enemyAttackedSounds.Length > 0)
                sfxAudioSource.PlayOneShot(
                    fightingEvent.enemyAttackedSounds[Random.Range(0, fightingEvent.enemyAttackedSounds.Length)]);
            comboCount = 0;
        }

        var score = poseMatchCheck.PoseScoring(fightingEvent.playerAttackSequence[playerAttackIndex]
            .playerAttack[eventExerciseDataIndex].poseDatas[poseDataIndex]);

        //if it returns -1 it means the player did not achieve a good pose
        if (score == -1)
            return;

        playerDamage += score;
        totalScore += score;
        poseDataIndex++;
    }

    private void ManagePuzzleEvent(EnvironmentPuzzleData puzzleEvent)
    {
        //if it reaches the end of the pose data list
        if (puzzleEvent.exerciseData[eventExerciseDataIndex].ExerciseToPerform.poseDatas.Count <= poseDataIndex)
        {
            eventExerciseDataIndex++;
            poseDataIndex = 0;
            hasPlayedDialogueAudio = false;

            if (puzzleEvent.exerciseData.Length != eventExerciseDataIndex) return;
            eventExerciseDataIndex = 0;
            gameEventsIndex++;
            hasSwappedMusicAudioSource = false;
            return;
        }

        if (poseDataIndex == 0 && !hasPlayedDialogueAudio)
        {
            hasPlayedDialogueAudio = true;
            dialogueAudioSource.PlayOneShot(puzzleEvent.exerciseData[eventExerciseDataIndex].VoiceLineToPlay);
            exerciseImage.sprite = puzzleEvent.exerciseData[eventExerciseDataIndex].SpriteToShow;
        }

        var score = poseMatchCheck.PoseScoring(puzzleEvent.exerciseData[eventExerciseDataIndex].ExerciseToPerform
            .poseDatas[poseDataIndex]);

        //if it returns -1 it means the player did not achieve a good pose
        if (score == -1)
            return;

        totalScore += score;
        poseDataIndex++;
    }

    private void ManageDialogueEvent(DialogueData dialogueEvent)
    {
        switch (dialogueAudioSource.isPlaying)
        {
            case false when !hasPlayedDialogueAudio:
                dialogueAudioSource.clip = dialogueEvent.DialogueClip;
                dialogueAudioSource.Play();
                hasPlayedDialogueAudio = true;
                break;
            case false when hasPlayedDialogueAudio:
                hasPlayedDialogueAudio = false;
                gameEventsIndex++;
                hasSwappedMusicAudioSource = false;
                break;
        }
    }

    private void SlowScoreIncreaseOverTime()
    {
        //TODO: make it so combo timer is updated here

        currentDisplayScore = Mathf.Lerp(currentDisplayScore, totalScore * scoreModifier, scoreUpdateSpeed);

        scoreText.text = Mathf.Floor(currentDisplayScore).ToString();

        enemyCurrentDisplayHealth = Mathf.Lerp(enemyCurrentDisplayHealth, enemyHealth, scoreUpdateSpeed);

        enemyHealthBar.value = enemyCurrentDisplayHealth;

        playerCurrentDisplayHealth = Mathf.Lerp(playerCurrentDisplayHealth, playerHealth, scoreUpdateSpeed);
    }
}