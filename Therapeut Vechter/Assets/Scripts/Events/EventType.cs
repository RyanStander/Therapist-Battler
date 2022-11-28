using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Defines the different event types to be used in event data in enumeration form
public enum EventType 
{
    EndLevel,
    
    UpdateComboScore,
    UpdateTotalScore,
    SetupTotalScore,
    
    //Enemy
    SetupEnemy,
    DamageEnemyVisuals,
    DamageEnemy,
    HideEnemy,
    
    CreatePlayerNormalAttack,
    CreatePlayerComboAttack,
    
    //Audio
    PlayDialogueAudio,
    DialogueAudioStatusUpdate,
    PlaySfxAudio,
    PlayMusicAudio,
}
