using UnityEngine;

namespace Game.Ball
{
    public class Model
    {

        
        public bool IsActive;
        public GameObject Face;
        public SpriteRenderer FaceSprite;
        
        public Color Color { get; }
        
        public Model(Color color, bool isActive = true)
        {
            Color = color;
            IsActive = isActive;
        }
    }
}