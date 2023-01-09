using UnityEngine;

namespace Exercises
{
    /// <summary>
    /// Holds the data of a pose
    /// </summary>
    [CreateAssetMenuAttribute(fileName = "PoseData", menuName = "Exercise Data/Pose Data")]
    public class PoseData : ScriptableObject
    {
        [Tooltip("How much scoring this will give in comparison to other poses")][Range(0, 1)] public float scoreModifier=1;

        [Header("Feet")] [Range(0, 1)] public float leftFootScoreValue = 0.2f;
        public bool leftFootMustMatchToProgress=false;
        public Quaternion leftFootRotation;
        [Range(0, 1)] public float rightFootScoreValue = 0.2f;
        public Quaternion rightFootRotation;
        public bool rightFootMustMatchToProgress=false;
        [Header("Lower Legs")] [Range(0, 1)] public float leftLowerLegScoreValue = 1;
        public Quaternion leftLowerLegRotation;
        public bool leftLowerLegMustMatchToProgress=true;
        [Range(0, 1)] public float rightLowerLegScoreValue = 1;
        public Quaternion rightLowerLegRotation;
        public bool rightLowerLegMustMatchToProgress=true;
        [Header("Upper Legs")] [Range(0, 1)] public float leftUpperLegScoreValue = 1;
        public Quaternion leftUpperLegRotation;
        public bool leftUpperLegMustMatchToProgress=true;
        [Range(0, 1)] public float rightUpperLegScoreValue = 1;
        public Quaternion rightUpperLegRotation;
        public bool rightUpperLegMustMatchToProgress=true;
        [Header("Spines")] [Range(0, 1)] public float pelvisScoreValue = 0.1f;
        public Quaternion pelvisRotation;
        public bool pelvisMustMatchToProgress=false;
        [Range(0, 1)] public float sternumScoreValue = 0.1f;
        public Quaternion sternumRotation;
        public bool sternumMustMatchToProgress=false;
    }
}