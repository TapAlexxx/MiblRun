using Scripts.Infrastructure.Services.Factories.Game;
using Scripts.Infrastructure.Services.Window;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Logic.WindowControls
{

    public class LoadNextLevelButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        
        private IGameFactory _gameFactory;
        private IWindowService _windowService;

        private void OnValidate()
        {
            if (!button) TryGetComponent(out button);
        }

        [Inject]
        public void Construct(IGameFactory gameFactory, IWindowService windowService)
        {
            _windowService = windowService;
            _gameFactory = gameFactory;
        }

        private void Start()
        {
            button.onClick.AddListener(LoadNext);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(LoadNext);
        }

        private void LoadNext()
        {
            _windowService.CloseOpened();
            _gameFactory.Player.GetComponent<Raycaster>().Activate();
        }
    }

}