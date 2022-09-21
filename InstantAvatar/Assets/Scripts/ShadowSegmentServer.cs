using UnityEngine;
using UnityEngine.PlayerLoop;

public class ShadowSegmentServer : SegmentServer
{
    private void Start()
    {
        port = 4244;
        // Debug.Log("Shadow started");
        startServer();        
    }
}