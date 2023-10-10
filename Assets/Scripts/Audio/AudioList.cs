using System.Collections.Generic;
using UnityEngine;

namespace Mercenary.Audio
{
    [System.Serializable]
    public class AudioList
    {
        [SerializeField]
        private List<Audio> audioList;
        public AudioList()
        {
            audioList = new List<Audio>();
        }

        public Audio GetAudioClip(string clipName)
        {
            //Get the respective audio from audioList based on the name
            foreach (Audio audio in audioList)
            {
                if(audio.IsName(clipName))
                {
                    return audio;
                }
            }

            return null;
        }
    }

}
