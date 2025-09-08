using UnityEngine;

namespace Sound
{
    [CreateAssetMenu(fileName = "SoundDatabase", menuName = "Audio/SoundDatabase")]
    public class SoundDatabase : ScriptableObject
    {
        public AudioClip[] audioClips;

        public AudioClip GetClipByName(string clipName)
        {
            foreach (var clip in audioClips)
            {
                if (clip.name == clipName)
                    return clip;
            }
            return null;
        }
    }
}