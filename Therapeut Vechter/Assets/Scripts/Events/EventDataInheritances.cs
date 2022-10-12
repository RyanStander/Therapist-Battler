using System.Collections.Generic;
using Exercises;
using UnityEngine;

//Event that informs subscribers of a debug log
public class SendDebugLog : EventData
{
    public readonly string debuglog;

    public SendDebugLog(string givenLog) : base(EventType.ReceiveDebug)
    {
        debuglog = givenLog;
    }
}
