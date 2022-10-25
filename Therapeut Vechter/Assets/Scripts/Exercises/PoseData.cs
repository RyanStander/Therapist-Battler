using UnityEngine;

namespace Exercises
{
    [CreateAssetMenuAttribute(fileName = "PoseData", menuName = "Exercise Data/Pose Data")]
    public class PoseData : ScriptableObject
    {
        [Tooltip("How much scoring this will give in comparison to other poses")][Range(0, 1)] public float scoreModifier=1;

        [Header("Feet")] [Range(0, 1)] public float leftFootScoreValue = 0.2f;
        public Quaternion leftFootRotation;
        [Range(0, 1)] public float rightFootScoreValue = 0.2f;
        public Quaternion rightFootRotation;
        [Header("Lower Legs")] [Range(0, 1)] public float leftLowerLegScoreValue = 1;
        public Quaternion leftLowerLegRotation;
        [Range(0, 1)] public float rightLowerLegScoreValue = 1;
        public Quaternion rightLowerLegRotation;
        [Header("Upper Legs")] [Range(0, 1)] public float leftUpperLegScoreValue = 1;
        public Quaternion leftUpperLegRotation;
        [Range(0, 1)] public float rightUpperLegScoreValue = 1;
        public Quaternion rightUpperLegRotation;
        [Header("Spines")] [Range(0, 1)] public float pelvisScoreValue = 0.1f;
        public Quaternion pelvisRotation;
        [Range(0, 1)] public float sternumScoreValue = 0.1f;
        public Quaternion sternumRotation;
    }
}