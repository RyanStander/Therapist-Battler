using System;
using System.Collections;
using System.Collections.Generic;
using GameEvents;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameEventDataHolder gameEventDataHolder;

    private Queue<BaseGameEvent> gameEvents;
    private BaseGameEvent currentGameEvent;

    private void Awake()
    {
        InitialiseGame();
    }

    private void InitialiseGame()
    {
        foreach (var gameEvent in gameEventDataHolder.gameEvents)
        {
            gameEvents.Enqueue(gameEvent);
        }
    }
    
    public void AdvanceLevel()
    {
        Debug.Log("Advancing level");
        currentGameEvent = gameEvents.Dequeue();
    }
    
    public void PerformNextActionInEvent()
    {
        Debug.Log("Moving to next action");
        //go on to next action

        if (currentGameEvent is EnvironmentPuzzleData environmentPuzzleData)
        {
            
        }
        
        //if no more actions
        //AdvanceLevel();
    }
}
