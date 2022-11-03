using Exercises;
using UnityEditor;
using UnityEngine;

namespace EditorTools
{
    /// <summary>
    /// saves the rotations of different body points to a scriptable object based on given data.
    /// </summary>
    public class ModelBodyPointsSaver : MonoBehaviour
    {
        [SerializeField] private ModelBodyPoints modelBodyPoints;
        [SerializeField] private PoseDataSet poseDataSetToSaveTo;
        [SerializeField] private bool addPoseToPoseDataSet = true;
        [TextArea][SerializeField] private string savePath = "Assets/ScriptableObjects/Exercises/Poses/";
        [SerializeField] private string poseSetSaveName = "PoseSet";
        [SerializeField] private string poseSaveName = "Pose";
        
        private void OnValidate()
        {
            //Make sure a model body point is connected
            if (modelBodyPoints==null)
                modelBodyPoints=GetComponent<ModelBodyPoints>();
        }

        public void SaveModelBodyPoints()
        {
            poseDataSetToSaveTo = AssetDatabase.LoadAssetAtPath<PoseDataSet>(savePath+poseSetSaveName+".asset");
            
            if (addPoseToPoseDataSet && poseDataSetToSaveTo == null)
            {
                Debug.LogWarning("No poseDataSet was selected/found and you wish you add it to the pose data set, either deselect the toggle or set a pose data set for this to work");
                return;
            }

            var poseData=ScriptableObject.CreateInstance<PoseData>();

            #region Setting Pose Data
            
            poseData.pelvisRotation = modelBodyPoints.pelvis.localRotation;
            poseData.sternumRotation = modelBodyPoints.sternum.localRotation;
            //Left parts
            poseData.leftFootRotation = modelBodyPoints.leftFoot.localRotation;
            poseData.leftLowerLegRotation = modelBodyPoints.leftLowerLeg.localRotation;
            poseData.leftUpperLegRotation = modelBodyPoints.leftUpperLeg.localRotation;
            //Right parts
            poseData.rightFootRotation = modelBodyPoints.rightFoot.localRotation;
            poseData.rightLowerLegRotation = modelBodyPoints.rightLowerLeg.localRotation;
            poseData.rightUpperLegRotation = modelBodyPoints.rightUpperLeg.localRotation;
            
            #endregion
            
            AssetDatabase.CreateAsset(poseData,savePath+poseSaveName+".asset");

            //Brings the project window to the front and focuses it
            EditorUtility.FocusProjectWindow();

            if (!addPoseToPoseDataSet || poseDataSetToSaveTo == null) 
                return;

            poseDataSetToSaveTo.poseDatas.Add(poseData);
                
            //Save the asset as it was modified
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();


        }

        public void CreatePoseDataSet()
        {
            var poseDataSet=ScriptableObject.CreateInstance<PoseDataSet>();
            
            AssetDatabase.CreateAsset(poseDataSet,savePath+poseSetSaveName+".asset");

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            
            poseDataSetToSaveTo = AssetDatabase.LoadAssetAtPath<PoseDataSet>(savePath+poseSetSaveName+".asset");
            
            //Brings the project window to the front and focuses it
            EditorUtility.FocusProjectWindow();
        }
    }
}
