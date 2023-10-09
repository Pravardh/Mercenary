using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mercenary.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioHandler : MonoBehaviour
    {
        [SerializeField]
        private AudioDataSO audioData;

        private AudioSource audioSource;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        public void PlayAudio(string audioClipName, bool randomize = false)
        {
            audioData.PlayAudio(audioClipName, audioSource, randomize);
        }

    }

}
