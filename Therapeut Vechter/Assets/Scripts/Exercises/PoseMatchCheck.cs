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

            if (totalPartsCorrect < minimumRequiredMatches) 
                return false;
            
            Debug.Log("Matched to pose!");
                
            return true;

        }
    }
}