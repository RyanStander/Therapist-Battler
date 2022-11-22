﻿//Event that informs subscribers of a debug log

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

    public UpdateComboScore(bool enableCombo,float comboTimer,int comboCount): base(EventType.UpdateComboScore)
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

    public SetupTotalScore(float maxScore): base(EventType.SetupTotalScore)
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

    public UpdateTotalScore(float score): base(EventType.UpdateTotalScore)
    {
        Score = score;
    }
}


 #region EnemyManager

/// <summary>
/// Sets the enemy displays
/// </summary>
public class SetupEnemy : EventData
{
    public readonly Sprite EnemySprite;
    public readonly float EnemyHealth;
    public readonly float EnemyHealthUpdateSpeed;
    public SetupEnemy(Sprite enemySprite,float enemyHealth, float enemyHealthUpdateSpeed) : base(EventType.SetupEnemy)
    {
        EnemySprite = enemySprite;
        EnemyHealth=enemyHealth;
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
public class CreatePlayerNormalAttack : EventData
{
    public readonly float Damage;

    public CreatePlayerNormalAttack(float damage): base(EventType.CreatePlayerNormalAttack)
    {
        Damage = damage;
    }
}

/// <summary>
/// Creates an attack effect for the player combo
/// </summary>
public class CreatePlayerComboAttack : EventData
{
    public readonly float Damage;

    public CreatePlayerComboAttack(float damage) : base(EventType.CreatePlayerComboAttack)
    {
        Damage = damage;
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

public class DialogueAudioStatusUpdate : EventData
{
    public readonly bool IsPlayingDialogue;
    public DialogueAudioStatusUpdate(bool isPlayingDialogue) : base(EventType.DialogueAudioStatusUpdate)
    {
        IsPlayingDialogue = isPlayingDialogue;
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

#endregion