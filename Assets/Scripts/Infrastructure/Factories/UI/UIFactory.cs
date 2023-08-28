using Infrastructure.Constants;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factories.UI
{
    public class UIFactory : IUIFactory
    {
        private DiContainer _container;
        private RectTransform _canvasRect;
        private GameObject _waitingHud;
        private GameObject _endgameHud;

        public UIFactory(DiContainer container)
        {
            _container = container;

            CreateRootCanvas();
        }

        private void CreateRootCanvas() =>
            _canvasRect = _container.InstantiatePrefabResource(ResourcePaths.ROOT_CANVAS).GetComponent<RectTransform>();

        public void CreateHUD() =>
            _container.InstantiatePrefabResource(ResourcePaths.HUD, _canvasRect);
    }
}