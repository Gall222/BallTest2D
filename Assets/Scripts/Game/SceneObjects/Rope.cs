using UnityEngine;

namespace Game.SceneObjects
{
    public class Rope : MonoBehaviour
    {
        public Transform ballBase;      // объект BallBase
        public Transform ball;          // объект Ball
        public LineRenderer ropeLine;   // Line Renderer для визуализации веревки
        public Vector3 targetPosition;  // цель для BallBase
        public float moveSpeed = 2f;    // скорость движения BallBase

        void Update()
        {
            // Плавно движем BallBase к targetPosition
            ballBase.position = Vector3.Lerp(ballBase.position, targetPosition, moveSpeed * Time.deltaTime);

            // Обновляем Line Renderer для веревки между BallBase и Ball
            ropeLine.SetPosition(0, ballBase.position);
            ropeLine.SetPosition(1, ball.position);
        }
    }
}