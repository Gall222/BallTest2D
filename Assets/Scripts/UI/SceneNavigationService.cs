using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace UI
{
    public class SceneNavigationService
    {
        public static Dictionary<Scenes, string> SceneNames = new Dictionary<Scenes, string>
        {
            [Scenes.Menu] = "Menu",
            [Scenes.Game] = "Game",
            [Scenes.End] = "End",
        };
        public enum Scenes
        {
            Menu,
            Game,
            End,
        }
        
        public static void LoadScene(Scenes scene)
        {
            SceneManager.LoadScene(SceneNames[scene]);
        }
    }
}