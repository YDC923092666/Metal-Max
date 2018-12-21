using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MetalMax
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public void PlayBGM(string filePath, string clipName, AudioSource audioSource)
        {
            var BGMClip = Resources.Load<AudioClip>(filePath + "/" + clipName);
            audioSource.clip = BGMClip;
            audioSource.Play();
        }

    }
}
