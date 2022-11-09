//Event that informs subscribers of a debug log

using UnityEngine;

public class SendDebugLog : EventData
{
    public readonly string Debuglog;

    public SendDebugLog(string givenLog) : base(EventType.ReceiveDebug)
    {
        Debuglog = givenLog;
    }
}

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
public class DamageEnemy : EventData
{
    public readonly float CurrentHealth;
    public DamageEnemy(float currentHealth) : base(EventType.DamageEnemy)
    {
        CurrentHealth = currentHealth;
    }
}

#endregion
