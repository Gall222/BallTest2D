using System;
using BallView = Game.Ball.View;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Pendulum
{
    public class View : MonoBehaviour
    {
        [SerializeField] private Transform rope;
        [SerializeField] private Transform ballPosition;
        [SerializeField] private float minRotateLimit = -50f;
        [SerializeField] private float maxRotateLimit = 50f;
        [SerializeField] private float motorSpeed = 50f;
        
        private HingeJoint2D _hinge;
        private JointMotor2D _motor;
        private bool _motorForwardRotate = true;
        private Rigidbody2D _rb;
        
        public event EventHandler DestroyPendulum;
        
        public Rigidbody2D Rb => _rb;
        public Transform Rope => rope;
        public Transform BallPosition => ballPosition;

        void Start()
        {
            _hinge = rope.GetComponent<HingeJoint2D>();
            _motor = _hinge.motor;
            _rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            var angle = _hinge.jointAngle;

            if (angle >= maxRotateLimit && _motorForwardRotate)
            {
                _motor.motorSpeed = -motorSpeed;
                _motorForwardRotate = false;
                _hinge.motor = _motor;
            } else if (angle <= minRotateLimit && !_motorForwardRotate)
            {
                _motor.motorSpeed = motorSpeed;
                _motorForwardRotate = true;
                _hinge.motor = _motor;
            }
        }

        private void OnDestroy()
        {
            DestroyPendulum?.Invoke(this, EventArgs.Empty);
        }
    }
}