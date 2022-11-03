using UnityEditor;
using UnityEngine;

namespace EditorTools
{
    /// <summary>
    /// This tooling is rather simple and is just an extension to the model body points save script, adds 2 buttons
    /// </summary>
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
