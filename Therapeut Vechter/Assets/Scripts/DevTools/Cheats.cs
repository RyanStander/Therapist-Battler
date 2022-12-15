using UnityEngine;

namespace DevTools
{
    public class Cheats
    {
#if UNITY_EDITOR
        private int skipExerciseCount;

        public bool SkipExercise()
        {
            if (Input.GetKey(KeyCode.S)&& skipExerciseCount==0)
            {
                skipExerciseCount++;
            }

            if (Input.GetKey(KeyCode.K)&& skipExerciseCount==1)
            {
                skipExerciseCount++;
            }
            
            if (Input.GetKey(KeyCode.I)&& skipExerciseCount==2)
            {
                skipExerciseCount++;
            }
            
            if (Input.GetKey(KeyCode.P)&& skipExerciseCount==3)
            {
                skipExerciseCount++;
            }

            //If it does not equal the amount, return false
            if (skipExerciseCount != 4) 
                return false;
            
            Debug.Log("Skipping exercise");
            //If it does equal the amount set to 0 and return true
            skipExerciseCount = 0;
            return true;

        }
        
#endif
    }
}