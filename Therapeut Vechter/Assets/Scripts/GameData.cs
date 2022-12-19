using System.Collections.Generic;
using Exercises;
using GameEvents;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance { get; private set; }

    public GameEventDataHolder currentLevel;
    public List<PoseDataSet> exercisesToExclude=new();

    private void Awake() 
    { 
        // If there is an instance, and it's not me, delete myself.
    
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
            DontDestroyOnLoad(gameObject);
        } 
    }
}
