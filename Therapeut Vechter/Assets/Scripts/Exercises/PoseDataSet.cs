using System.Collections.Generic;
using UnityEngine;

namespace Exercises
{
    /// <summary>
    /// Holds a set of pose datas for an exercise, this would be considered a full exercise when completed
    /// </summary>
    [CreateAssetMenu(menuName = "Exercise Data/Pose Data Set")]
    public class PoseDataSet : ScriptableObject
    {
        [Tooltip("The score obtained from completing the exercise, this is the maximum")]
        public float scoreValue = 100;
        public List<PoseData> poseDatas=new List<PoseData>();
    }
}
