using System;
using Exercises;
using UnityEditor;
using UnityEngine;

namespace EditorTools
{
    public class ModelBodyPointsSaver : MonoBehaviour
    {
        [SerializeField] private ModelBodyPoints modelBodyPoints;
        [TextArea][SerializeField] private string savePath = "Assets/ScriptableObjects/Exercises/Poses/";
        [SerializeField] private string saveName = "Pose";

        private void OnValidate()
        {
            if (modelBodyPoints==null)
            {
                modelBodyPoints=GetComponent<ModelBodyPoints>();
            }
        }

        public void SaveModelBodyPoints()
        {
            var poseData=ScriptableObject.CreateInstance<PoseData>();

            #region Setting Pose Data
            
            poseData.pelvisRotation = modelBodyPoints.pelvis.rotation;
            poseData.sternumRotation = modelBodyPoints.sternum.rotation;
            //Left parts
            poseData.leftFootRotation = modelBodyPoints.leftFoot.rotation;
            poseData.leftLowerLegRotation = modelBodyPoints.leftLowerLeg.rotation;
            poseData.leftUpperLegRotation = modelBodyPoints.leftUpperLeg.rotation;
            //Right parts
            poseData.rightFootRotation = modelBodyPoints.rightFoot.rotation;
            poseData.rightLowerLegRotation = modelBodyPoints.rightLowerLeg.rotation;
            poseData.rightUpperLegRotation = modelBodyPoints.rightUpperLeg.rotation;
            
            #endregion
            
            AssetDatabase.CreateAsset(poseData,savePath+saveName+".asset");

            //Brings the project window to the front and focuses it
            EditorUtility.FocusProjectWindow();
        }
    }
}
