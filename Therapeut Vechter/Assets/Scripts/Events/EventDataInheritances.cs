﻿//Event that informs subscribers of a debug log

using Exercises;
using FMODUnity;
using UnityEngine;

/// <summary>
/// Updates the displayed combo score ui
/// </summary>
public class UpdateComboScore : EventData
{
    public readonly bool EnableCombo;
    public readonly float ComboTimer;
    public readonly int ComboCount;

    public UpdateComboScore(bool enableCombo, float comboTimer, int comboCount) : base(EventType.UpdateComboScore)
    {
        EnableCombo = enableCombo;
        ComboTimer = comboTimer;
        ComboCount = comboCount;
    }
}

/// <summary>
/// Setup the displayed score ui
/// </summary>
public class SetupTotalScore : EventData
{
    public readonly float MaxScore;

    public SetupTotalScore(float maxScore) : base(EventType.SetupTotalScore)
    {
        MaxScore = maxScore;
    }
}

/// <summary>
/// Updates the displayed score ui
/// </summary>
public class UpdateTotalScore : EventData
{
    public readonly float Score;

    public UpdateTotalScore(float score) : base(EventType.UpdateTotalScore)
    {
        Score = score;
    }
}

public class EndLevel : EventData
{
    public readonly float PlayerScore;
    public readonly float MaxScore;
    public readonly int StarsAchieved;

    public EndLevel(float playerScore, float maxScore, int starsAchieved) : base(EventType.EndLevel)
    {
        PlayerScore = playerScore;
        MaxScore = maxScore;
        StarsAchieved = starsAchieved;
    }
}

#region Player

public class DamagePlayer : EventData
{
    public readonly float PlayerDamage;

    public DamagePlayer(float playerDamage) : base(EventType.DamagePlayer)
    {
        PlayerDamage = playerDamage;
    }
}

#endregion


#region EnemyManager

/// <summary>
/// Sets the enemy displays
/// </summary>
public class SetupEnemy : EventData
{
    public readonly GameObject EnemyGameObject;
    public readonly float EnemyHealth;
    public readonly float EnemyHealthUpdateSpeed;

    public SetupEnemy(GameObject enemyGameObject, float enemyHealth, float enemyHealthUpdateSpeed) : base(EventType.SetupEnemy)
    {
        EnemyGameObject = enemyGameObject;
        EnemyHealth = enemyHealth;
        EnemyHealthUpdateSpeed = enemyHealthUpdateSpeed;
    }
}

/// <summary>
/// Hide all the enemy ui
/// </summary>
public class HideEnemy : EventData
{
    public HideEnemy() : base(EventType.HideEnemy)
    {
    }
}

/// <summary>
/// Gives the enemy damage effects
/// </summary>
public class DamageEnemyVisuals : EventData
{
    public readonly float DamageToTake;

    public DamageEnemyVisuals(float damageToTake) : base(EventType.DamageEnemyVisuals)
    {
        DamageToTake = damageToTake;
    }
}

public class DamageEnemy : EventData
{
    public readonly float EnemyDamage;

    public DamageEnemy(float enemyDamage) : base(EventType.DamageEnemy)
    {
        EnemyDamage = enemyDamage;
    }
}

#endregion

#region Effects

/// <summary>
/// Creates an attack effect for the player 
/// </summary>
public class CreateNormalAttack : EventData
{
    public readonly float Damage;
    public readonly EventReference OnHitSfx;
    public readonly bool IsPlayerAttack;
    public readonly GameObject AttackEffect;

    public CreateNormalAttack(float damage, EventReference onHitSfx, bool isPlayerAttack=true,GameObject attackEffect=null) : base(EventType.CreateNormalAttack)
    {
        Damage = damage;
        OnHitSfx = onHitSfx;
        IsPlayerAttack = isPlayerAttack;
        AttackEffect = attackEffect;
    }
}

#endregion

#region Audio

public class PlayDialogueAudio : EventData
{
    public readonly EventReference EventSoundPath;

    public PlayDialogueAudio(EventReference eventSoundPath) : base(EventType.PlayDialogueAudio)
    {
        EventSoundPath = eventSoundPath;
    }
}

public class PlayExerciseDialogueAudio : EventData
{
    public readonly EventReference EventSoundPath;

    public PlayExerciseDialogueAudio(EventReference eventSoundPath) : base(EventType.PlayExerciseDialogueAudio)
    {
        EventSoundPath = eventSoundPath;
    }
}

public class DialogueAudioStatusUpdate : EventData
{
    public readonly bool IsPlayingDialogue;

    public DialogueAudioStatusUpdate(bool isPlayingDialogue) : base(EventType.DialogueAudioStatusUpdate)
    {
        IsPlayingDialogue = isPlayingDialogue;
    }
}

public class StopDialogue : EventData
{
    public StopDialogue() : base(EventType.StopDialogue)
    {
    }
}

public class PlaySfxAudio : EventData
{
    public readonly EventReference EventSoundPath;

    public PlaySfxAudio(EventReference eventSoundPath) : base(EventType.PlaySfxAudio)
    {
        EventSoundPath = eventSoundPath;
    }
}

public class PlayMusicAudio : EventData
{
    public readonly EventReference EventSoundPath;

    public PlayMusicAudio(EventReference eventSoundPath) : base(EventType.PlayMusicAudio)
    {
        EventSoundPath = eventSoundPath;
    }
}

public class AdvanceMusicStage : EventData
{
    public AdvanceMusicStage(): base(EventType.AdvanceMusicStage)
    {
        
    }
}

public class PlayAmbienceAudio : EventData
{
    public readonly EventReference EventSoundPath;
    
    public PlayAmbienceAudio(EventReference eventSoundPath) : base(EventType.PlayAmbienceAudio)
    {
        EventSoundPath = eventSoundPath;
    }
}

#endregion

public class ExcludeExercise : EventData
{
    public readonly PoseDataSet ExerciseToExclude;
    public ExcludeExercise(PoseDataSet exerciseToExclude) : base(EventType.ExcludeExercise)
    {
        ExerciseToExclude = exerciseToExclude;
    }
}

public class ResetExcludedExercises : EventData
{
    public ResetExcludedExercises() : base(EventType.ResetExcludedExercises)
    {
    }
}