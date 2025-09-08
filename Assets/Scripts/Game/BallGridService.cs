using System;
using System.Collections;
using Game.Ball;
using Game.StaticData;
using Management;
using Sound;
using UI;
using UnityEngine;

namespace Game
{
    public class BallGridService
    {
        private const int MaxBallInGrid = 9;
        private const int OneLineSize = 3;

        public Presenter[] ballHolder = new Presenter[MaxBallInGrid];
        private RectTransform[] _ballGridPositions;
        private readonly Camera _cam;
        
        private readonly int[][] _linesForCheck = new int[][]
        {
            new[] {0, 1, 2}, // горизонтали
            new[] {3, 4, 5},
            new[] {6, 7, 8},
            new[] {0, 3, 6}, // вертикали
            new[] {1, 4, 7},
            new[] {2, 5, 8},
            new[] {0, 4, 8}, // диагонали
            new[] {2, 4, 6}
        };

        public BallGridService(RectTransform[] ballGridPositions)
        {
            _ballGridPositions = ballGridPositions;
            _cam = Camera.main;
        }

        public void CheckNewPositions()
        {
            for (int i = MaxBallInGrid - 1 - OneLineSize; i >= 0; i--)
            {
                if (ballHolder[i] == null && ballHolder[i + OneLineSize] != null)
                {
                    ballHolder[i] = ballHolder[i + OneLineSize];
                    ballHolder[i + OneLineSize] = null;
                }
            }
        }

        public void SaveBallPosition(Presenter ballPresenter)
        {
            foreach (var gridPosition in _ballGridPositions)
            {
                if (IsOverlapping(gridPosition, ballPresenter.View.transform))
                {
                    var index = Array.IndexOf(_ballGridPositions, gridPosition);
                    ballHolder[index] = ballPresenter;
                }
            }
        }

        public void CheckAllLines()
        {
            foreach (var line in _linesForCheck)
                CheckLine(line[0], line[1], line[2]);

            IsGridFull();
        }

        private void IsGridFull()
        {
            if (ballHolder[6] != null && ballHolder[7] != null && ballHolder[8] != null)
            {
                SceneNavigationService.LoadScene(SceneNavigationService.Scenes.End);
            }
        }

        private void CheckLine(int idx1, int idx2, int idx3)
        {
            Presenter p1 = ballHolder[idx1];
            Presenter p2 = ballHolder[idx2];
            Presenter p3 = ballHolder[idx3];

            // Сброс лиц для всех в линии, чтобы не было застарелых лиц
            if (p1 != null) p1.SetFace(BallData.Faces.Bored);
            if (p2 != null) p2.SetFace(BallData.Faces.Bored);
            if (p3 != null) p3.SetFace(BallData.Faces.Bored);

            // Тройка одной цвет
            if (p1 != null && p2 != null && p3 != null)
            {
                if (p1.Model.Color == p2.Model.Color && p2.Model.Color == p3.Model.Color)
                {
                    CollectThreeBallInLine(idx1, idx2, idx3, p1, p2, p3);
                }
                return;
            }

            // Пара с пустой ячейкой - меняем лица двоим одинакового цвета
            if (p1 != null && p2 != null && p3 == null && p1.Model.Color == p2.Model.Color)
            {
                SetNewFace(BallData.Faces.Nice, p1, p2);
            }
            else if (p1 != null && p3 != null && p2 == null && p1.Model.Color == p3.Model.Color)
            {
                SetNewFace(BallData.Faces.Nice, p1, p3);
            }
            else if (p2 != null && p3 != null && p1 == null && p2.Model.Color == p3.Model.Color)
            {
                SetNewFace(BallData.Faces.Nice, p2, p3);
            }
        }

        /** All 3 has one color */
        private void CollectThreeBallInLine(int idx1, int idx2, int idx3, Presenter p1, Presenter p2, Presenter p3)
        {
            ScoreManager.Instance.AddScore(p1.Model.Color);
            SetNewFace(BallData.Faces.Happy, p1, p2, p3);
            
            p1.View.StartCoroutine(FadeAndDestroy(p1, p2, p3));
            ballHolder[idx1] = ballHolder[idx2] = ballHolder[idx3] = null;
            CheckNewPositions();
        }

        private void SetNewFace(BallData.Faces faces, params Presenter[] presenters)
        {
            foreach (var presenter in presenters)
            {
                presenter.SetFace(faces);
            }
        }

        private bool IsOverlapping(RectTransform gridPosition, Transform ball)
        {
            Vector2 screenPoint = _cam.WorldToScreenPoint(ball.position);
            return RectTransformUtility.RectangleContainsScreenPoint(gridPosition, screenPoint);
        }

        private IEnumerator FadeAndDestroy(params Presenter[] presenters)
        {
            SoundService.PlaySound("Disappear");
            foreach (var presenter in presenters)
            {
                GameObject.Instantiate(DataManager.GetDisappearBallEffect(), 
                    presenter.View.transform.position, Quaternion.identity, presenter.View.transform);
            }
            
            float duration = 1f;
            float elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                
                foreach (var presenter in presenters)
                {
                    SpriteRenderer sr = presenter.View.SpriteRenderer;
                    Color color = sr.color;
                    float alpha = Mathf.Lerp(1f, 0f, elapsed / duration);
                    sr.color = new Color(color.r, color.g, color.b, alpha);
                    var faceColor = presenter.Model.FaceSprite.color;
                    presenter.Model.FaceSprite.color = new Color(faceColor.r, faceColor.g, faceColor.b, alpha);
                }
                
                yield return null;
            }
            
            foreach (var presenter in presenters)
            {
                presenter.DestroyBall();
            }

            CheckNewPositions();
        }
    }
}