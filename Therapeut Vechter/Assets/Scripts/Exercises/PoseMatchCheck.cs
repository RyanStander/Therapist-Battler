using System;
using UnityEngine;

namespace Exercises
{
    public class PoseMatchCheck : MonoBehaviour
    {
        [SerializeField] private ModelBodyPoints modelBodyPoints;
        [Range(0, 90)] [SerializeField] private float angleTolerance = 5f;

        private int totalPartsCorrect;

        [Range(0, 8)] [SerializeField] private int minimumRequiredMatches=6;

        private float scoring;
        private float maxAngle = 180;

        public bool PoseMatches(PoseData poseData)
        {
            totalPartsCorrect = 0;

            #region Left Parts

            //Upper Legs
            if (Quaternion.Angle(poseData.leftUpperLegRotation, modelBodyPoints.leftUpperLeg.localRotation) < angleTolerance)
            {
                totalPartsCorrect++;
            }
            
            //Lower Legs
            if (Quaternion.Angle(poseData.leftLowerLegRotation, modelBodyPoints.leftLowerLeg.localRotation) < angleTolerance)
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
            if (Quaternion.Angle(poseData.rightUpperLegRotation, modelBodyPoints.rightUpperLeg.localRotation) < angleTolerance)
            {
                totalPartsCorrect++;
            }
            
            //Lower Legs
            if (Quaternion.Angle(poseData.rightLowerLegRotation, modelBodyPoints.rightLowerLeg.localRotation) < angleTolerance)
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

            Debug.Log("Player achieved score of: "+ PoseScoring(poseData));
            
            if (totalPartsCorrect < minimumRequiredMatches) 
                return false;

            return true;

        }

        //Returns a percentile scoring that the player obtains for their exercise
        public float PoseScoring(PoseData poseData)
        {
            scoring = 0;
            totalPartsCorrect = 0;

            #region Left Parts

            //Upper Legs
            if (Quaternion.Angle(poseData.leftUpperLegRotation, modelBodyPoints.leftUpperLeg.localRotation) < angleTolerance)
            {
                totalPartsCorrect++;
            }
            
            //Lower Legs
            if (Quaternion.Angle(poseData.leftLowerLegRotation, modelBodyPoints.leftLowerLeg.localRotation) < angleTolerance)
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
            if (Quaternion.Angle(poseData.rightUpperLegRotation, modelBodyPoints.rightUpperLeg.localRotation) < angleTolerance)
            {
                totalPartsCorrect++;
            }
            
            //Lower Legs
            if (Quaternion.Angle(poseData.rightLowerLegRotation, modelBodyPoints.rightLowerLeg.localRotation) < angleTolerance)
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

            //Left
            scoring+= 1-Quaternion.Angle(poseData.leftUpperLegRotation, modelBodyPoints.leftUpperLeg.localRotation)/maxAngle;
            scoring+= 1-Quaternion.Angle(poseData.leftLowerLegRotation, modelBodyPoints.leftLowerLeg.localRotation)/maxAngle;
            scoring+= 1-Quaternion.Angle(poseData.leftFootRotation, modelBodyPoints.leftFoot.localRotation)/maxAngle;
            
            //Right
            scoring+= 1-Quaternion.Angle(poseData.rightUpperLegRotation, modelBodyPoints.rightUpperLeg.localRotation)/maxAngle;
            scoring+= 1-Quaternion.Angle(poseData.rightLowerLegRotation, modelBodyPoints.rightLowerLeg.localRotation)/maxAngle;
            scoring+= 1-Quaternion.Angle(poseData.rightUpperLegRotation, modelBodyPoints.rightFoot.localRotation)/maxAngle;
            
            //Upper body
            scoring+= 1-Quaternion.Angle(poseData.pelvisRotation, modelBodyPoints.pelvis.localRotation)/maxAngle;
            scoring+= 1-Quaternion.Angle(poseData.sternumRotation, modelBodyPoints.sternum.localRotation)/maxAngle;
            
            if (totalPartsCorrect < minimumRequiredMatches)
                return -1;
            
            return scoring;
        }
    }
}