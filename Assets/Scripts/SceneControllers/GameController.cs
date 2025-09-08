using System;
using Game;
using Game.Ball;
using PendulumPresenter = Game.Pendulum.Presenter;
using PartitionPresenter = Game.Partition.Presenter;
using UI;
using UnityEngine;
using Management;
using Sound;
using Presenter = Game.Partition.Presenter;

namespace SceneControllers
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private SceneData sceneData;
        private PartitionPresenter _partitionPresenter;
        private PendulumPresenter _pendulumPresenter;
        private BallGridService _ballGridService;
        private float _ballSize;
        private readonly float _ballSizeCoef = 9f;

        private void Awake()
        {
            SoundService.Initialize(sceneData.AudioSource, DataManager.GetSoundDatabase());
            _ballGridService = new BallGridService(sceneData.BallGridPositions);
            var partitionPoints = sceneData.PartitionVerticalSpawnPoints;
            float worldDistance = Vector3.Distance(partitionPoints[0].position, partitionPoints[1].position);
            float normalized = worldDistance / Screen.height;
            _ballSize = normalized * _ballSizeCoef;
            
            _pendulumPresenter = PresenterManager.NewPendulum(sceneData.PendulumSpawnPoint.position, _ballSize);
            _partitionPresenter = PresenterManager.GetPartitionPresenter();
            _partitionPresenter.Create(sceneData.PartitionVerticalSpawnPoints, sceneData.PartitionBottomSpawnPoint, sceneData.PartitionsHolder);
        }
        
        private void OnSaveBallPosition(object sender, EventArgs e)
        {
            _ballGridService.SaveBallPosition(_pendulumPresenter.BallPresenter);
            _ballGridService.CheckAllLines();
            _pendulumPresenter.CreateBall(_ballSize);
        }
        
        private void OnBallDeath(object sender, EventArgs e)
        {
            ScoreManager.Instance.ReduceScore();
            _pendulumPresenter.CreateBall(_ballSize);
        }

        private void OnEnable()
        {
            _pendulumPresenter.SaveBallPosition += OnSaveBallPosition;
            _pendulumPresenter.BallDeath += OnBallDeath;
        }

        private void OnDisable()
        {
            _pendulumPresenter.SaveBallPosition -= OnSaveBallPosition;
            _pendulumPresenter.BallDeath -= OnBallDeath;
        }
    }
}