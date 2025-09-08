using System.Collections.Generic;
using UnityEngine;

namespace Game.StaticData
{
    public class BallData
    {
        public static List<Color> Colors = new List<Color>
        {
            Color.red,
            Color.green,
            Color.blue,
        };

        public enum Faces
        {
            Bored,
            Nice,
            Happy,
        }
        
        public static GameObject GetFace(Faces face)
        {
            return _faces[face];
        }
        
        private static Dictionary<Faces, GameObject> _faces = new Dictionary<Faces, GameObject>
        {
            [Faces.Bored] = Resources.Load<GameObject>("Prefabs/Faces/Bored"),
            [Faces.Nice] = Resources.Load<GameObject>("Prefabs/Faces/Nice"),
            [Faces.Happy] = Resources.Load<GameObject>("Prefabs/Faces/Happy")
        };
    }
}