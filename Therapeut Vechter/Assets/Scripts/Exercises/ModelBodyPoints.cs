using UnityEngine;

namespace Exercises
{
    public class ModelBodyPoints : MonoBehaviour
    {
        [Header("Feet")] public Transform leftFoot;
        public Transform rightFoot;
        [Header("Lower Legs")] public Transform leftLowerLeg;
        public Transform rightLowerLeg;
        [Header("Upper Legs")] public Transform leftUpperLeg;
        public Transform rightUpperLeg;
        [Header("Spines")] public Transform pelvis;
        public Transform sternum;
    }
}
