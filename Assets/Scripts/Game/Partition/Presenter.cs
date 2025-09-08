using UnityEngine;

namespace Game.Partition
{
    public class Presenter
    {
        private GameObject _partitionPrefab = Resources.Load<GameObject>("Prefabs/Partition");
        private GameObject _partitionBottomPrefab = Resources.Load<GameObject>("Prefabs/PartitionBottom");

        public void Create(Transform[] verticalPoints, Transform bottomPoint, Transform parent)
        {
            foreach (var point in verticalPoints)
            {
                Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector2(point.position.x, point.position.y));
                
                var partition = Object.Instantiate(_partitionPrefab, parent);
                partition.transform.position = worldPosition;
            }
            
            Vector2 bottomWorldPosition = Camera.main.ScreenToWorldPoint(new Vector2(bottomPoint.position.x, bottomPoint.position.y));
            var bottom = Object.Instantiate(_partitionBottomPrefab, parent);
            bottom.transform.position = bottomWorldPosition;
        }
    }
}