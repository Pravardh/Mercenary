using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mercenary.Audio
{
    [System.Serializable]
    public class Audio
    {
        [SerializeField]
        private string audioClipName;

        [SerializeField]
        private List<AudioClip> audioClips;

        public bool IsName(string name)
        {
            if(audioClipName.Equals(name)) 
                return true;

            return false;
        }

        public string GetName()
        {
            return audioClipName;
        }

        public AudioClip GetClip()
        {
            return audioClips[0];
        }

        public void Play(AudioSource audioSource, bool random = false)
        {
            if (random)
            {
                audioSource.clip = audioClips[Random.Range(0, audioClips.Count)];
                audioSource.Play();
                return;
            }

            audioSource.clip = audioClips[0];
            audioSource.Play();

        }
    }


}
