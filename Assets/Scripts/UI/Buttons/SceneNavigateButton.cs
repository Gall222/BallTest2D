using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Buttons
{
    public class SceneNavigateButton : IButton, IPointerClickHandler
    {
        [SerializeField] private SceneNavigationService.Scenes sceneName;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            SceneNavigationService.LoadScene(sceneName);
        }
    }
}