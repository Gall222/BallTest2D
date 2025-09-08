using System.Collections;
using System.Collections.Generic;
using System.Linq;
using PendulumPresenter = Game.Pendulum.Presenter;
using Management;
using UI;
using UnityEngine;
using BallPresenter = Game.Ball.Presenter;

namespace SceneControllers
{
    public class StartMenuController : MonoBehaviour
    {
        [SerializeField] private SceneData sceneData;
        
        private PendulumPresenter _pendulumPresenter;
        private List<PendulumPresenter> _pendulums = new List<PendulumPresenter>();
        private BallPresenter _ballPresenter;
        
        public float moveSpeed = 0.3f; 
        public float minSpawnTime = 4f; 
        public float maxSpawnTime = 5f; 
        public int maxPendulumCount = 6; 

        void Start()
        {
            CreatePendulum();
            StartCoroutine(SpawnPendulum());
        }
        
        private IEnumerator SpawnPendulum()
        {
            while (true)
            {
                float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
                yield return new WaitForSeconds(waitTime);
                
                if (_pendulums.Count >= maxPendulumCount)
                {
                    var first = _pendulums.First();
                    var position = sceneData.PendulumSpawnPoint.position;
                    Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector2(position.x, position.y));
                    first.View.transform.position = worldPosition;
                    _pendulums.Remove(first);
                    _pendulums.Add(first);
                }
                else
                {
                    CreatePendulum();
                }
            }
        }

        private void CreatePendulum()
        {
            var pendulum =  PresenterManager.NewPendulum(sceneData.PendulumSpawnPoint.position);
            
            _pendulums.Add(pendulum);
        }
        
        void Update()
        {
            foreach (var pendulum in _pendulums)
            {
                Vector2 newPos = pendulum.View.Rb.position + Vector2.left * (moveSpeed * Time.fixedDeltaTime);
                pendulum.View.Rb.MovePosition(newPos);
            }
        }

        private void OnDestroy()
        {
            foreach (var pendulum in _pendulums)
            {
                pendulum.Disable();
            }
        }
    }
}