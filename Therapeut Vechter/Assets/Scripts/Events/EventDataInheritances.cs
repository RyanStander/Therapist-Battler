//Event that informs subscribers of a debug log
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
