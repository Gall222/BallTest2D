using Sound;
using UnityEngine;
using BallPresenter = Game.Ball.Presenter;
using PendulumPresenter = Game.Pendulum.Presenter;
using PartitionPresenter = Game.Partition.Presenter;

namespace Management
{
    public class PresenterManager
    {
        private static PartitionPresenter _partitionPresenter;
        private static PendulumPresenter _pendulumPresenter;
        private static BallPresenter _ballPresenter;
        private const float DefaultSize = 1f;

        public static PartitionPresenter GetPartitionPresenter()
        {
            return _partitionPresenter ??= new PartitionPresenter();
        }
        
        public static PendulumPresenter NewPendulum(Vector2 position, float ballSize = DefaultSize)
        {
            var pendulumPresenter = new PendulumPresenter(position, ballSize);
            
            return pendulumPresenter;
        }

        public static BallPresenter NewBall(float ballSize ,bool isRandomFace = false)
        {
            var ballPresenter = new BallPresenter(ballSize, isRandomFace);
            
            return ballPresenter;
        }
    }
}