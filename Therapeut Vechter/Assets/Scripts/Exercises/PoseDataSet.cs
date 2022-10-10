using UnityEngine;

namespace Exercises
{
    [CreateAssetMenu(menuName = "Exercise Data/Pose Data Set")]
    public class PoseDataSet : ScriptableObject
    { 
        public PoseData[] poseDatas;
    }
}
