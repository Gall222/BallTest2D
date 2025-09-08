using System;
using Game.StaticData;
using UnityEngine.InputSystem;
using Management;
using UnityEngine;

namespace Game.Ball
{
    public class Presenter
    {
        private Model _model;
        private View _view;
        private Camera _cam;
        
        public Presenter(float ballSize ,bool isRandomFace = false)
        {
            _cam = Camera.main;
            _view = UnityEngine.Object.Instantiate(DataManager.GetBallPrefab()).GetComponent<View>();
            
            int randomIndex = UnityEngine.Random.Range(0, BallData.Colors.Count);
            var color = BallData.Colors[randomIndex];
             _view.GetComponent<SpriteRenderer>().color = color;

            BallData.Faces face = isRandomFace
                ? (BallData.Faces)UnityEngine.Random.Range(0, Enum.GetValues(typeof(BallData.Faces)).Length)
                : BallData.Faces.Bored;
            
            _model = new Model(color);
            _model.Face = UnityEngine.Object.Instantiate(BallData.GetFace(face), _view.FacePosition);
            
            _view.transform.localScale = Vector3.one * ballSize;
        }
        
        public Model Model => _model;
        public View View => _view;
        
        public bool IsBallClicked()
        {
            Vector2 screenPos = Pointer.current.position.ReadValue();
            Vector3 worldPos = _cam.ScreenToWorldPoint(screenPos);

            RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
            
            return hit.collider != null && _model.IsActive;
        }

        public void DestroyBall()
        {
            GameObject.Destroy(_model.Face);
            GameObject.Destroy(_view.gameObject);
            _model = null;
            _view = null;
        }

        public void SetFace(BallData.Faces face)
        {
            GameObject.Destroy(_model.Face);
            _model.Face = UnityEngine.Object.Instantiate(BallData.GetFace(face), _view.FacePosition);
            _model.FaceSprite = _model.Face.GetComponent<SpriteRenderer>();
        }
    }
}