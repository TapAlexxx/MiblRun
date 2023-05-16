using Scripts.Infrastructure.Services.Factories.UIFactory;
using Scripts.Window;
using UnityEngine;
using Zenject;

namespace Scripts.Infrastructure.Services.Window
{
    public class WindowService : IWindowService
    {
        private IUIFactory _uiFactory;
        private RectTransform _lastOpened;

        [Inject]
        public void Constructor(IUIFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void Open(WindowTypeId windowTypeId)
        {
            _lastOpened = _uiFactory.CrateWindow(windowTypeId);
        }

        public void CloseOpened()
        {
            Object.Destroy(_lastOpened.gameObject);
        }
    }
}