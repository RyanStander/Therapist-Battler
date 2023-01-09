using System;
using UnityEngine;

namespace Exercises
{
    /// <summary>
    /// Checks how well the characters pose matches to the given pose data
    /// </summary>
    public class PoseMatchCheck : MonoBehaviour
    {
        [SerializeField] private ModelBodyPoints modelBodyPoints;

        [Tooltip("How many degrees a limb's rotation can be off and still be accepted")] [Range(0, 90)] [SerializeField]
        private float angleTolerance = 5f;

        //The score that the player achieves based on their match skills
        private float scoring;

        //The max angle that a limb could be off by, used to calculate the scoring
        private const float MaxAngle = 180;

        //Returns a percentile scoring that the player obtains for their exercise
        public float PoseScoring(PoseData poseData)
        {
            scoring = 0;

            #region Left Parts

            //Upper Legs
            if (poseData.leftUpperLegMustMatchToProgress &&
                !(Quaternion.Angle(poseData.leftUpperLegRotation, modelBodyPoints.leftUpperLeg.localRotation) <
                  angleTolerance))
            {
                return -1;
            }

            //Lower Legs
            if (poseData.leftLowerLegMustMatchToProgress &&
                !(Quaternion.Angle(poseData.leftLowerLegRotation, modelBodyPoints.leftLowerLeg.localRotation) <
                  angleTolerance))
            {
                return -1;
            }

            //Feet
            if (poseData.leftFootMustMatchToProgress &&
                !(Quaternion.Angle(poseData.leftFootRotation, modelBodyPoints.leftFoot.localRotation) < angleTolerance))
            {
                return -1;
            }

            #endregion

            #region Right Parts

            //Upper Legs
            if (poseData.rightUpperLegMustMatchToProgress && !(Quaternion.Angle(poseData.rightUpperLegRotation, modelBodyPoints.rightUpperLeg.localRotation) <
                angleTolerance))
            {
                return -1;
            }

            //Lower Legs
            if (poseData.rightLowerLegMustMatchToProgress &&!(Quaternion.Angle(poseData.rightLowerLegRotation, modelBodyPoints.rightLowerLeg.localRotation) <
                angleTolerance))
            {
                return -1;
            }

            //Feet
            if (poseData.rightFootMustMatchToProgress &&(Quaternion.Angle(poseData.rightFootRotation, modelBodyPoints.rightFoot.localRotation) < angleTolerance))
            {
                return -1;
            }

            #endregion

            #region Body Parts

            //Pelvis
            if (poseData.pelvisMustMatchToProgress && !(Quaternion.Angle(poseData.pelvisRotation, modelBodyPoints.pelvis.localRotation) < angleTolerance))
            {
                return -1;
            }

            //Sternum
            if (poseData.sternumMustMatchToProgress && !(Quaternion.Angle(poseData.sternumRotation, modelBodyPoints.sternum.localRotation) < angleTolerance))
            {
                return -1;
            }

            #endregion

            #region Scoring Limbs

            //Left
            scoring += 1 - Quaternion.Angle(poseData.leftUpperLegRotation, modelBodyPoints.leftUpperLeg.localRotation) /
                MaxAngle * poseData.leftUpperLegScoreValue;
            scoring += 1 - Quaternion.Angle(poseData.leftLowerLegRotation, modelBodyPoints.leftLowerLeg.localRotation) /
                MaxAngle * poseData.leftLowerLegScoreValue;
            scoring += 1 - Quaternion.Angle(poseData.leftFootRotation, modelBodyPoints.leftFoot.localRotation) /
                MaxAngle * poseData.leftFootScoreValue;

            //Right
            scoring +=
                1 - Quaternion.Angle(poseData.rightUpperLegRotation, modelBodyPoints.rightUpperLeg.localRotation) /
                MaxAngle * poseData.rightUpperLegScoreValue;
            scoring +=
                1 - Quaternion.Angle(poseData.rightLowerLegRotation, modelBodyPoints.rightLowerLeg.localRotation) /
                MaxAngle * poseData.rightLowerLegScoreValue;
            scoring += 1 - Quaternion.Angle(poseData.rightUpperLegRotation, modelBodyPoints.rightFoot.localRotation) /
                MaxAngle * poseData.rightFootScoreValue;

            //Upper body
            scoring += 1 - Quaternion.Angle(poseData.pelvisRotation, modelBodyPoints.pelvis.localRotation) / MaxAngle *
                poseData.pelvisScoreValue;
            scoring += 1 - Quaternion.Angle(poseData.sternumRotation, modelBodyPoints.sternum.localRotation) /
                MaxAngle * poseData.sternumScoreValue;

            #endregion

            return (scoring / 8);
        }
    }
}