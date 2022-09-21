using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// ReSharper disable All

public class ShadowSegmentMessageHandler : SegmentMessageHandler
{
    // unnecessary class; helps to keep script instances in Unity separated

    void Awake()
    {
        // do nothing; only primary segment message handler should change variables
    }

    new public void adjustVerticalPosition()
    {
        
    }
}