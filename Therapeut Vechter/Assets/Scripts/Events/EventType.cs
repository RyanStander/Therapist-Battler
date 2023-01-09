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
    
    //Player
    DamagePlayer,
    
    //Effects
    CreateNormalAttack,

    //Audio
    PlayDialogueAudio,
    PlayExerciseDialogueAudio,
    DialogueAudioStatusUpdate,
    StopDialogue,
    PlaySfxAudio,
    PlayMusicAudio,
    PlayAmbienceAudio,
    
    //Exercise
    ExcludeExercise,
    ResetExcludedExercises,
}
