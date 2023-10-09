using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mercenary.Audio
{
    [System.Serializable]
    [CreateAssetMenu(fileName = "New AudioData", menuName = "Mercernary/Data/Create AudioData")]
    public class AudioDataSO : ScriptableObject
    {
        [SerializeField]
        private AudioList audios;

        public void PlayAudio(string audioName, AudioSource audioSource, bool randomize)
        {
            
            audios.GetAudioClip(audioName).Play(audioSource, randomize);

        }
    }

}
