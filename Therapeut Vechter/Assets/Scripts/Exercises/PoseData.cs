using UnityEngine;

namespace Exercises
{
    [CreateAssetMenuAttribute(fileName = "PoseData",menuName = "Exercise Data/Pose Data")]
    public class PoseData : ScriptableObject
    {

        [Header("Feet")]
        public Quaternion leftFootRotation;
        public Quaternion rightFootRotation;
        [Header("Lower Legs")]
        public Quaternion leftLowerLegRotation;
        public Quaternion rightLowerLegRotation;
        [Header("Upper Legs")]
        public Quaternion leftUpperLegRotation;
        public Quaternion rightUpperLegRotation;
        [Header("Spines")]
        public Quaternion pelvisRotation;
        public Quaternion sternumRotation;
    }
}