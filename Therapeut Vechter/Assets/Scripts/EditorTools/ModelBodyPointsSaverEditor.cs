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
            
            var saveModelBodyPoints = (ModelBodyPointsSaver) target;
            if (GUILayout.Button("Save Model Body Points"))
            {
                saveModelBodyPoints.SaveModelBodyPoints();
            }
        }
    }
}
