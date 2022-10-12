using System.Collections.Generic;
using UnityEngine;

namespace Exercises
{
    [CreateAssetMenu(menuName = "Exercise Data/Pose Data Set")]
    public class PoseDataSet : ScriptableObject
    { 
        public List<PoseData> poseDatas=new List<PoseData>();
    }
}
