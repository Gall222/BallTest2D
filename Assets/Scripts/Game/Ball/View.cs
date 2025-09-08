using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Game.Ball
{
    public class View : MonoBehaviour
    {
        [SerializeField] private Transform facePosition;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float fallingDumping = 0.5f;
        
        public event EventHandler StopFalling;
        public event EventHandler DeathZoneEnter;
        public event EventHandler CollisionEnter;
        
        public bool isFalling = false;
        public Transform FacePosition => facePosition;
        public Rigidbody2D Rb => rb;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        public float FallingDumping => fallingDumping;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("DeathZone"))
            {
                DeathZoneEnter?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                CollisionEnter?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Update()
        {
            if (isFalling == true) CheckFinishPosition();
        }

        private void CheckFinishPosition()
        {
            if (!isFalling) return;
            if (!rb.IsSleeping()) return;
            StopFalling?.Invoke(this, EventArgs.Empty);
            isFalling = false;
        }
    }
}