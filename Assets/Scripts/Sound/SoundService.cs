using UnityEngine;

namespace Sound
{
    public static class SoundService
    {
        private static AudioSource _audioSource;
        private static SoundDatabase _soundDatabase;
        
        public static void Initialize(AudioSource audioSource, SoundDatabase soundDatabase)
        {
            _audioSource = audioSource;
            _soundDatabase = soundDatabase;
        }
        
        public static void PlaySound(string soundKey)
        {
            if (_audioSource == null)
            {
                Debug.LogWarning("SoundService: AudioSource не инициализирован");
                return;
            }
            if (_soundDatabase == null)
            {
                Debug.LogWarning("SoundService: SoundDatabase не задан");
                return;
            }

            AudioClip clip = _soundDatabase.GetClipByName(soundKey);
            if (clip != null)
            {
                _audioSource.PlayOneShot(clip);
            }
            else
            {
                Debug.LogWarning($"SoundService: Клипа с именем '{soundKey}' не найдено");
            }
        }
    }
}