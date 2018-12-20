using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax {
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager mInstance;

        public static AudioManager Instance
        {
            get
            {
                if (!mInstance)
                {
                    mInstance = new GameObject("AudioManager").AddComponent<AudioManager>();
                }

                return mInstance;
            }
        }

        public void PlaySound()
        {

        }

    }
}
