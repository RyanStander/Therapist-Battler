using UnityEngine;

namespace Exercises
{
    [CreateAssetMenu(menuName = "Exercise Data/Pose Data")]
    public class PoseData : ScriptableObject
    {
        [Header("Feet")] public Vector3 leftFootPosition;
        public Quaternion leftFootRotation;
        public Vector3 rightFootPosition;
        public Quaternion rightFootRotation;
        [Header("Lower Legs")] public Vector3 leftLowerLegPosition;
        public Quaternion leftLowerLegRotation;
        public Vector3 rightLowerLegPosition;
        public Quaternion rightLowerLegRotation;
        [Header("Upper Legs")] public Vector3 leftUpperLegPosition;
        public Quaternion leftUpperLegRotation;
        public Vector3 rightUpperLegPosition;
        public Quaternion rightUpperLegRotation;
        [Header("Spines")] public Vector3 pelvisPosition;
        public Quaternion pelvisRotation;
        public Vector3 sternumPosition;
        public Quaternion sternumRotation;
    }
}