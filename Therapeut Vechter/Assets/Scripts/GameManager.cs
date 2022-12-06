using System.Linq;
using Exercises;
using FMODUnity;
using GameEvents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private Image backgroundTransitionImage;
    [SerializeField] private Slider[] playerHealthBars;

    [Header("Scoring")] [Range(0, 1)] [SerializeField]
    private float scoreUpdateSpeed = 0.5f;

    [SerializeField] private float comboDuration = 3;

    [Tooltip("The damage modifier that is applied to how high the combo count is")] [SerializeField]
    private float baseComboDamage = 20;

    private float playerDamage;

    #endregion
    
    #region Private Fields

    //the score that the player has achieved
    private float totalScore;

    //the score achieved during a score
    private float currentExerciseScore;

    //the last time stamp of a combo
    private float comboTimeStamp;

    //The total combo count
    private int comboCount;

    //The score that is currently displayed (the 2 values are lerped together to make a slow increase)
    private float currentDisplayScore;

    //Used for when setting up an event
    private bool hasPerformedFirstTimeSetup;

    //Used to display the new background
    private bool transitionToNewBackground;

    #region Audio Data

    private bool isPlayingDialogueAudio;

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

    //used to identify how far the current posedata index is
    private int exercisePerformIndex;

    #endregion

    #region Fighting Data

    private float playerCurrentDisplayHealth;
    private float playerHealth = 100;
    private float enemyHealth;
    private bool isDead;

    #endregion

    #region Feedback timer

    [SerializeField] private float timeUntilRepeatExerciseName = 10;

    private float exerciseRepeatTimeStamp;

    private bool exerciseTimerIsRunning;

    #endregion

    #endregion

    #region Handling End of Game

    [Tooltip("In Seconds")] [SerializeField]
    private float endGameDelay;

    private float endGameDelayTimestamp;
    private bool endGameTimerIsRunning;

    #endregion

    private float maxScore;

    #region Runtime

    private void OnEnable()
    {
        EventManager.currentManager.Subscribe(EventType.DamageEnemy, OnEnemyTakeDamage);
        EventManager.currentManager.Subscribe(EventType.DamagePlayer, OnPlayerTakeDamage);
        EventManager.currentManager.Subscribe(EventType.DialogueAudioStatusUpdate, OnDialogueAudioStatusUpdate);
    }

    private void OnDisable()
    {
        EventManager.currentManager.Unsubscribe(EventType.DamageEnemy, OnEnemyTakeDamage);
        EventManager.currentManager.Unsubscribe(EventType.DamagePlayer, OnPlayerTakeDamage);
        EventManager.currentManager.Unsubscribe(EventType.DialogueAudioStatusUpdate, OnDialogueAudioStatusUpdate);
    }

    private void Awake()
    {
        //Sets the starting values for the game
        InitialiseGame();
    }

    private void FixedUpdate()
    {
        RunLevel();

        SlowScoreIncreaseOverTime();

        TransitionBackgrounds();
    }

    #endregion


    private void InitialiseGame()
    {
        if (GameData.Instance != null && GameData.Instance.currentLevel != null)
            gameEventDataHolder = GameData.Instance.currentLevel;


        ResetVariables();

        scoreText.text = 0.ToString();

        foreach (var playerHealthBar in playerHealthBars)
        {
            playerHealthBar.maxValue = playerHealth;
            playerHealthBar.value = playerHealth;
        }

        backgroundImage.sprite = gameEventDataHolder.startingBackground;

        SetupLevelScore();
    }

    //Manages the functionality of the level
    private void RunLevel()
    {
        if (endGameTimerIsRunning && endGameDelayTimestamp <= Time.time)
        {
            var starsAchieved = 0;
            //Call event to show end game screen;
            //full stars
            if (currentDisplayScore > maxScore * 0.9f)
            {
                starsAchieved = 3;
            }
            //two stars
            else if (currentDisplayScore > maxScore * 2 / 3)
            {
                starsAchieved = 2;
            }
            //one star
            else if (currentDisplayScore > maxScore * 1 / 3)
            {
                starsAchieved = 1;
            }

            EventManager.currentManager.AddEvent(new EndLevel(totalScore, maxScore, starsAchieved));
        }

        //Check if it has reached the list or player has died
        if (gameEventsIndex >= gameEventDataHolder.gameEvents.Length || playerHealth <= 0)
        {
            if (endGameTimerIsRunning) return;
            //Start game end
            endGameDelayTimestamp = Time.time + endGameDelay;
            endGameTimerIsRunning = true;

            return;
        }


        //Swaps song
        if (gameEventDataHolder.gameEvents[gameEventsIndex].OverrideCurrentlyPlayingMusic &&
            !hasSwappedMusicAudioSource)
        {
            EventManager.currentManager.AddEvent(
                new PlayMusicAudio(gameEventDataHolder.gameEvents[gameEventsIndex].OverrideMusic));

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

    #region Fighting Event

    private void ManageFightingEvent(FightingData fightingEvent)
    {
        //if the enemy has just appeared
        SetupFightingEvent(fightingEvent);

        //if enemy dies
        if (enemyHealth < 1)
        {
            ResetVariables();
            gameEventsIndex++;
            return;
        }

        if (isDead)
            return;

        TryPlayFightingExerciseDialogue(fightingEvent);

        //Checks if the current Exercise has been completed
        if (fightingEvent.playerAttackSequence[playerAttackIndex].playerAttack.poseDatas.Count <= poseDataIndex)
            CompletedFightingExercise(fightingEvent);

        ManageComboDamage(fightingEvent);

        RepeatExerciseNameAfterTime(fightingEvent.playerAttackSequence[playerAttackIndex].exerciseName);

        CheckPosePerformance(fightingEvent);
    }

    private void SetupFightingEvent(FightingData fightingEvent)
    {
        if (hasPerformedFirstTimeSetup)
            return;

        exerciseImage.gameObject.SetActive(false);
        hasPerformedFirstTimeSetup = true;

        //set background image
        if (fightingEvent.BackgroundSprite != null)
            StartBackgroundTransition(fightingEvent.BackgroundSprite);


        enemyHealth = fightingEvent.enemyHealth;

        EventManager.currentManager.AddEvent(new SetupEnemy(fightingEvent.enemySprite, fightingEvent.enemyHealth,
            scoreUpdateSpeed));
    }

    private void TryPlayFightingExerciseDialogue(FightingData fightingEvent)
    {
        if (hasPlayedDialogueAudio)
            return;

        hasPlayedDialogueAudio = true;

        EventManager.currentManager.AddEvent(
            exercisePerformIndex == 0
                ? new PlaySfxAudio(fightingEvent.playerAttackSequence[playerAttackIndex].startingVoiceLine)
                : new PlaySfxAudio(fightingEvent.playerAttackSequence[playerAttackIndex].exerciseName));
    }

    private void CompletedFightingExercise(FightingData fightingEvent)
    {
        exercisePerformIndex++;

        hasPlayedDialogueAudio = false;
        poseDataIndex = 0;

        var currentScoreCalculation =
            (currentExerciseScore /
             fightingEvent.playerAttackSequence[playerAttackIndex].playerAttack.poseDatas.Count) *
            fightingEvent.playerAttackSequence[playerAttackIndex].playerAttack.scoreValue;

        //add to score
        totalScore += currentScoreCalculation;

        currentExerciseScore = 0;
        exerciseTimerIsRunning = false;

        comboTimeStamp = Time.time + comboDuration;
        comboCount++;
        EventManager.currentManager.AddEvent(new UpdateComboScore(true, comboDuration, comboCount));

        //Fire a player attack
        EventManager.currentManager.AddEvent(new CreateNormalAttack(currentScoreCalculation,
            fightingEvent.enemyHurtSound));

        if (fightingEvent.playerAttackSequence[playerAttackIndex].timesToPerform <= exercisePerformIndex)
        {
            playerAttackIndex++;

            exercisePerformIndex = 0;

            //Fire an enemy attack
            EventManager.currentManager.AddEvent(new CreateNormalAttack(fightingEvent.enemyDamage,
                fightingEvent.enemyAttackSound, false, fightingEvent.enemyDamageEffect));

            //We reset the attack index so that it starts the first attack again
            if (fightingEvent.playerAttackSequence.Length <= playerAttackIndex)
            {
                playerAttackIndex = 0;
            }

            EventManager.currentManager.AddEvent(
                new PlaySfxAudio(fightingEvent.playerAttackSequence[playerAttackIndex].exerciseName));
        }
    }

    private void ManageComboDamage(FightingData fightingEvent)
    {
        var comboDamage = (baseComboDamage * ((float)comboCount / (10 - comboCount)));

        //have a combo timer running, depending on how many combos they get, they get higher damage
        if ((comboTimeStamp <= Time.time && comboCount > 0) || comboDamage > enemyHealth)
        {
            if (comboDamage > enemyHealth)
                isDead = true;

            EventManager.currentManager.AddEvent(new CreateComboAttack(comboDamage, fightingEvent.enemyHurtSound));

            totalScore += comboDamage;

            comboCount = 0;
        }
    }

    private void CheckPosePerformance(FightingData fightingEvent)
    {
        var score = poseMatchCheck.PoseScoring(fightingEvent.playerAttackSequence[playerAttackIndex]
            .playerAttack.poseDatas[poseDataIndex]);

        //if it returns -1 it means the player did not achieve a good pose
        if (score == -1)
            return;

        currentExerciseScore += score;
        poseDataIndex++;
    }
    
    #endregion
    
    private void ManagePuzzleEvent(EnvironmentPuzzleData puzzleEvent)
    {
        if (!hasPerformedFirstTimeSetup)
        {
            if (puzzleEvent.BackgroundSprite != null)
                StartBackgroundTransition(puzzleEvent.BackgroundSprite);
            hasPerformedFirstTimeSetup = true;
        }

        //if it reaches the end of the pose data list
        if (puzzleEvent.exerciseData[eventExerciseDataIndex].ExerciseToPerform.poseDatas.Count <= poseDataIndex)
        {
            var currentScoreCalculation =
                (currentExerciseScore /
                 puzzleEvent.exerciseData[eventExerciseDataIndex].ExerciseToPerform.poseDatas.Count) *
                puzzleEvent.exerciseData[eventExerciseDataIndex].ExerciseToPerform.scoreValue;
            //add to score
            totalScore += currentScoreCalculation;

            EventManager.currentManager.AddEvent(new UpdateTotalScore(currentScoreCalculation));
            currentExerciseScore = 0;

            exerciseTimerIsRunning = false;
            hasPlayedDialogueAudio = false;

            exercisePerformIndex++;
            if (exercisePerformIndex >= puzzleEvent.exerciseData[eventExerciseDataIndex].timesToPerform)
            {
                exercisePerformIndex = 0;
                eventExerciseDataIndex++;
            }

            poseDataIndex = 0;

            if (puzzleEvent.exerciseData.Length != eventExerciseDataIndex) return;

            ResetVariables();
            gameEventsIndex++;
            return;
        }

        if (poseDataIndex == 0 && !hasPlayedDialogueAudio)
        {
            hasPlayedDialogueAudio = true;

            EventManager.currentManager.AddEvent(
                exercisePerformIndex == 0
                    ? new PlaySfxAudio(puzzleEvent.exerciseData[eventExerciseDataIndex].StartingVoiceLineToPlay)
                    : new PlaySfxAudio(puzzleEvent.exerciseData[eventExerciseDataIndex].VoiceLineToPlay));

            //If there is no image chosen, the exercise will not display
            if (puzzleEvent.exerciseData[eventExerciseDataIndex].SpriteToShow == true)
            {
                exerciseImage.sprite = puzzleEvent.exerciseData[eventExerciseDataIndex].SpriteToShow;
                exerciseImage.gameObject.SetActive(true);
            }
            else
            {
                exerciseImage.gameObject.SetActive(false);
            }
        }

        RepeatExerciseNameAfterTime(puzzleEvent.exerciseData[eventExerciseDataIndex].VoiceLineToPlay);

        var score = poseMatchCheck.PoseScoring(puzzleEvent.exerciseData[eventExerciseDataIndex].ExerciseToPerform
            .poseDatas[poseDataIndex]);

        //if it returns -1 it means the player did not achieve a good pose
        if (score == -1)
            return;

        currentExerciseScore += score;
        EventManager.currentManager.AddEvent(new UpdateTotalScore(score));
        poseDataIndex++;
    }

    private void ManageDialogueEvent(DialogueData dialogueEvent)
    {
        if (!hasPerformedFirstTimeSetup)
        {
            if (dialogueEvent.BackgroundSprite != null)
                StartBackgroundTransition(dialogueEvent.BackgroundSprite);
            hasPerformedFirstTimeSetup = true;
        }

        switch (isPlayingDialogueAudio)
        {
            case false when !hasPlayedDialogueAudio:
                EventManager.currentManager.AddEvent(new PlayDialogueAudio(dialogueEvent.EventPath));
                hasPlayedDialogueAudio = true;
                isPlayingDialogueAudio = true;
                break;
            case false when hasPlayedDialogueAudio:
                ResetVariables();
                gameEventsIndex++;
                break;
        }
    }

    #region Extra Functions

    private void SetupLevelScore()
    {
        foreach (var gameEvent in gameEventDataHolder.gameEvents)
        {
            switch (gameEvent)
            {
                case EnvironmentPuzzleData environmentPuzzleData:
                {
                    maxScore += environmentPuzzleData.exerciseData.Sum(exerciseData =>
                        exerciseData.ExerciseToPerform.scoreValue * exerciseData.timesToPerform);
                    break;
                }
                case FightingData fightingData:
                {
                    maxScore += fightingData.enemyHealth;
                    break;
                }
            }
        }

        EventManager.currentManager.AddEvent(new SetupTotalScore(maxScore));
    }

    private void RepeatExerciseNameAfterTime(EventReference eventReference)
    {
        //Set a timestamp to repeat the exercise if it is 0
        if (!exerciseTimerIsRunning)
        {
            exerciseTimerIsRunning = true;
            exerciseRepeatTimeStamp = Time.time + timeUntilRepeatExerciseName;
        }


        if (!(exerciseRepeatTimeStamp < Time.time)) return;

        EventManager.currentManager.AddEvent(new PlayDialogueAudio(eventReference));
        exerciseTimerIsRunning = false;
    }

    #endregion

    //Resets the main variables
    private void ResetVariables()
    {
        exerciseImage.gameObject.SetActive(false);

        EventManager.currentManager.AddEvent(new HideEnemy());

        hasSwappedMusicAudioSource = false;
        hasPlayedDialogueAudio = false;
        hasPerformedFirstTimeSetup = false;
        eventExerciseDataIndex = 0;
        poseDataIndex = 0;
        playerAttackIndex = 0;
        exercisePerformIndex = 0;
        exerciseTimerIsRunning = false;
        isDead = false;
        EventManager.currentManager.AddEvent(new UpdateComboScore(false, 0, 0));
    }

    private void SlowScoreIncreaseOverTime()
    {
        currentDisplayScore = Mathf.Lerp(currentDisplayScore, totalScore, scoreUpdateSpeed);

        scoreText.text = Mathf.Floor(currentDisplayScore).ToString();

        playerCurrentDisplayHealth = Mathf.Lerp(playerCurrentDisplayHealth, playerHealth, scoreUpdateSpeed);

        foreach (var playerHealthBar in playerHealthBars)
        {
            playerHealthBar.value = playerCurrentDisplayHealth;
        }
    }


    #region Background Transition

    private void StartBackgroundTransition(Sprite newBackground)
    {
        transitionToNewBackground = true;
        backgroundTransitionImage.sprite = backgroundImage.sprite;
        backgroundTransitionImage.gameObject.SetActive(true);
        backgroundImage.sprite = newBackground;
        //reset scale
        backgroundTransitionImage.transform.localScale = new Vector3(1, 1, 1);

        //reset color
        var c = backgroundTransitionImage.color;
        c.a = 1;
        backgroundTransitionImage.color = c;
    }

    private void TransitionBackgrounds()
    {
        if (!transitionToNewBackground) return;
        backgroundTransitionImage.transform.localScale += new Vector3(0.01f, 0.01f, 0);
        var c = backgroundTransitionImage.color;
        c.a -= 0.01f;
        if (c.a < 0.01f)
        {
            transitionToNewBackground = false;
            backgroundTransitionImage.gameObject.SetActive(false);
        }

        backgroundTransitionImage.color = c;
    }

    #endregion

    #region On Events

    private void OnEnemyTakeDamage(EventData eventData)
    {
        if (eventData is DamageEnemy damageEnemy)
        {
            enemyHealth -= damageEnemy.EnemyDamage;
        }
        else
        {
            Debug.Log("EventData of type DamageEnemy was not of type DamageEnemy.");
        }
    }

    private void OnPlayerTakeDamage(EventData eventData)
    {
        if (eventData is DamagePlayer damagePlayer)
        {
            playerHealth -= damagePlayer.PlayerDamage;
        }
        else
        {
            Debug.Log("EventData of type DamagePlayer was not of type DamagePlayer.");
        }
    }

    private void OnDialogueAudioStatusUpdate(EventData eventData)
    {
        if (eventData is DialogueAudioStatusUpdate audioStatusUpdate)
        {
            isPlayingDialogueAudio = audioStatusUpdate.IsPlayingDialogue;
        }
    }

    #endregion
}