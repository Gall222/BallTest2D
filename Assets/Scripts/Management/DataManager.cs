using Sound;
using UI;
using UnityEngine;

namespace Management
{
    /**
     * Load prefabs and data from scene
     */
    public class DataManager
    {
        private static GameObject _ballPrefab;
        private static GameObject _pendulumPrefab;
        private static GameObject _disappearBallEffect;
        private static SoundDatabase _soundDatabase;

        public static GameObject GetBallPrefab()
        {
            if (_ballPrefab == null)
            {
                _ballPrefab = Resources.Load<GameObject>("Prefabs/Ball");
            }
            
            return _ballPrefab;
        }

        public static GameObject GetPendulumPrefab()
        {
            if (_pendulumPrefab == null)
            {
                _pendulumPrefab = Resources.Load<GameObject>("Prefabs/Pendulum");
            }
            
            return _pendulumPrefab;
        }

        public static GameObject GetDisappearBallEffect()
        {
            if (_disappearBallEffect == null)
            {
                _disappearBallEffect = Resources.Load<GameObject>("Prefabs/DisappearEffect");
            }
            
            return _disappearBallEffect;
        }

        public static SoundDatabase GetSoundDatabase()
        {
            if (_soundDatabase == null)
            {
                _soundDatabase = Resources.Load<SoundDatabase>("StaticData/SoundDatabase");
            }
            
            return _soundDatabase;
        }
    }
}