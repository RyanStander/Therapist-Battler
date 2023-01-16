using Exercises;
using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameEvents
{
    [System.Serializable]
    public class PlayerAttackSequence
    {
        public EventReference startingVoiceLine;
        [FormerlySerializedAs("exerciseName")] public EventReference randomVoiceLine;
        public PoseDataSet playerAttack;
        public int timesToPerform = 1;
    }
}