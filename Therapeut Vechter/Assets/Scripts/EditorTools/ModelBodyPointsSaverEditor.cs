using Exercises;
using UnityEditor;
using UnityEngine;

namespace EditorTools
{
    [CustomEditor(typeof(ModelBodyPointsSaver))]
    public class ModelBodyPointsSaverEditor : Editor
    {
        
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            
            //Get the save model body points script
            var saveModelBodyPoints = (ModelBodyPointsSaver) target;
            
            if (GUILayout.Button("Create holder for poses"))
            {
                saveModelBodyPoints.CreatePoseDataSet();
            }
            
            if (GUILayout.Button("Save Model Body Points"))
            {
                saveModelBodyPoints.SaveModelBodyPoints();
            }
        }
    }
}
