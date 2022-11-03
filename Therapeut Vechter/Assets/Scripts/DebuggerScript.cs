using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggerScript : MonoBehaviour
{
    private bool debuggerActive=true;
    [SerializeField] private GameManager gameManager;

    private void Update()
    {
        if (!debuggerActive)
            return;
        
        CallPerformNextAction();
    }

    private void CallPerformNextAction()
    {
        //if (Input.GetKeyDown(KeyCode.A))
            //gameManager.PerformNextActionInEvent();
    }
}
