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

        //The total parts that are considered matched, checked every frame
        private int totalPartsCorrect;

        //The minimum required matches to be considered a pass
        [Range(0, 8)] [SerializeField] private int minimumRequiredMatches = 6;

        //The score that the player achieves based on their match skills
        private float scoring;

        //The max angle that a limb could be off by, used to calculate the scoring
        private const float MaxAngle = 180;

        //Returns a percentile scoring that the player obtains for their exercise
        public float PoseScoring(PoseData poseData)
        {
            scoring = 0;
            totalPartsCorrect = 0;

            #region Left Parts

            //Upper Legs
            if (Quaternion.Angle(poseData.leftUpperLegRotation, modelBodyPoints.leftUpperLeg.localRotation) <
                angleTolerance)
            {
                totalPartsCorrect++;
            }

            //Lower Legs
            if (Quaternion.Angle(poseData.leftLowerLegRotation, modelBodyPoints.leftLowerLeg.localRotation) <
                angleTolerance)
            {
                totalPartsCorrect++;
            }

            //Feet
            if (Quaternion.Angle(poseData.leftFootRotation, modelBodyPoints.leftFoot.localRotation) < angleTolerance)
            {
                totalPartsCorrect++;
            }

            #endregion

            #region Right Parts

            //Upper Legs
            if (Quaternion.Angle(poseData.rightUpperLegRotation, modelBodyPoints.rightUpperLeg.localRotation) <
                angleTolerance)
            {
                totalPartsCorrect++;
            }

            //Lower Legs
            if (Quaternion.Angle(poseData.rightLowerLegRotation, modelBodyPoints.rightLowerLeg.localRotation) <
                angleTolerance)
            {
                totalPartsCorrect++;
            }

            //Feet
            if (Quaternion.Angle(poseData.rightFootRotation, modelBodyPoints.rightFoot.localRotation) < angleTolerance)
            {
                totalPartsCorrect++;
            }

            #endregion

            #region Body Parts

            //Pelvis
            if (Quaternion.Angle(poseData.pelvisRotation, modelBodyPoints.pelvis.localRotation) < angleTolerance)
            {
                totalPartsCorrect++;
            }

            //Sternum
            if (Quaternion.Angle(poseData.sternumRotation, modelBodyPoints.sternum.localRotation) < angleTolerance)
            {
                totalPartsCorrect++;
            }

            #endregion

            //If there are not enough parts, return -1
            if (totalPartsCorrect < minimumRequiredMatches)
                return -1;

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

            var bodyPartScoring = poseData.leftUpperLegScoreValue + poseData.leftLowerLegScoreValue +
                                  poseData.leftFootScoreValue + poseData.rightUpperLegScoreValue +
                                  poseData.rightLowerLegScoreValue + poseData.rightFootScoreValue +
                                  poseData.pelvisScoreValue + poseData.sternumScoreValue;
            
            return (scoring /8);
        }
    }
}