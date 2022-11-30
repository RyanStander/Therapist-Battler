using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMODUnity.STOP_MODE;

namespace Audio
{
    public class Snapshot : MonoBehaviour
    {
        private EventInstance instance;

        public EventReference fmodEvent;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                instance = RuntimeManager.CreateInstance(fmodEvent);
                instance.start();
                
            }
            else if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                instance.release();
            }
        }
    }
}