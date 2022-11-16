using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Defines the different event types to be used in event data in enumeration form
public enum EventType 
{
    ReceiveDebug,
    
    UpdateComboScore,
    
    //Enemy
    SetupEnemy,
    DamageEnemyVisuals,
    DamageEnemy,
    HideEnemy,
    
    CreatePlayerNormalAttack,
    
    //Audio
    PlayDialogueAudio,
    DialogueAudioStatusUpdate,
    PlaySfxAudio,
    PlayMusicAudio,
}
