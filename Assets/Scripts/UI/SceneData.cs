using UnityEngine;

namespace UI
{
    public class SceneData : MonoBehaviour
    {
        [SerializeField] private Transform pendulumSpawnPoint;
        [SerializeField] private Transform[] partitionVerticalSpawnPoints;
        [SerializeField] private Transform partitionBottomSpawnPoint;
        [SerializeField] private Transform partitionsHolder;
        [SerializeField] private RectTransform[] ballGridPositions;
        [SerializeField] private AudioSource audioSource;
        
        public Transform PendulumSpawnPoint => pendulumSpawnPoint;
        public Transform[] PartitionVerticalSpawnPoints => partitionVerticalSpawnPoints;
        public Transform PartitionBottomSpawnPoint => partitionBottomSpawnPoint;
        
        public Transform PartitionsHolder => partitionsHolder;
        public RectTransform[] BallGridPositions => ballGridPositions;
        public AudioSource AudioSource => audioSource;
    }
}