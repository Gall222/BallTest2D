using System;
using Management;
using Sound;
using Unity.VisualScripting;
using UnityEngine;
using BallPresenter = Game.Ball.Presenter;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

namespace Game.Pendulum
{
    public class Presenter
    {
        public BallPresenter BallPresenter;
        private View _view;
        private PlayerInputActions _inputActions;
        private FixedJoint2D _joint;
        
        public event EventHandler SaveBallPosition;
        public event EventHandler BallDeath;
        public View View => _view;
        public Presenter(Vector2 position, float ballSize)
        {
            _inputActions = new PlayerInputActions();
           
            _view = UnityEngine.Object.Instantiate(DataManager.GetPendulumPrefab()).GetComponent<View>();
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(new Vector2(position.x, position.y));
            _view.transform.position = worldPosition;

            _view.DestroyPendulum += OnPendulumDestroy;
            CreateBall(ballSize);
        }

        public void CreateBall(float ballSize)
        {
            BallPresenter = PresenterManager.NewBall(ballSize);
            BallPresenter.View.transform.parent = _view.transform;
            BallPresenter.View.transform.position = _view.BallPosition.position;
            _joint = BallPresenter.View.AddComponent<FixedJoint2D>();
            _joint.connectedBody = _view.Rope.GetComponent<Rigidbody2D>();
            Enable();
        }
        
        private void OnBallClick(InputAction.CallbackContext context)
        {
            if (BallPresenter.IsBallClicked())
            {
                BallPresenter.Model.IsActive = false;
                BallPresenter.View.isFalling = true;
                Object.Destroy(_joint);
                BallPresenter.View.Rb.linearDamping = BallPresenter.View.FallingDumping;
            }
        }

        public void OnStopFalling(object sender, EventArgs e)
        {
            Disable();
            SaveBallPosition?.Invoke(this, EventArgs.Empty);
        }

        public void OnDeathZoneEnter(object sender, EventArgs e)
        { 
            Disable();
            BallPresenter.DestroyBall();
            BallDeath?.Invoke(this, EventArgs.Empty);
        }

        public void OnPendulumDestroy(object sender, EventArgs e)
        {
            _view.DestroyPendulum -= OnPendulumDestroy;
            Disable();
        }

        public void OnCollisionEnter(object sender, EventArgs e)
        {
            SoundService.PlaySound("Hit");
        }
        
        public void Enable()
        {
            _inputActions.Player.Enable();
            _inputActions.Player.Click.performed += OnBallClick;
            BallPresenter.View.StopFalling += OnStopFalling;
            BallPresenter.View.DeathZoneEnter += OnDeathZoneEnter;
            BallPresenter.View.CollisionEnter += OnCollisionEnter;
        }

        public void Disable()
        {
            _inputActions.Player.Disable();
            _inputActions.Player.Click.performed -= OnBallClick;
            BallPresenter.View.StopFalling -= OnStopFalling;
            BallPresenter.View.DeathZoneEnter -= OnDeathZoneEnter;
            BallPresenter.View.CollisionEnter -= OnCollisionEnter;

        }
    }
}