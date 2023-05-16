using Scripts.Infrastructure.Services.Factories.Game;
using Scripts.Infrastructure.Services.PersistenceProgress;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Logic.WindowControls
{

    public class RestartLevelButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        
        private IGameFactory _gameFactory;
        private IPersistenceProgressService _persistenceProgressService;

        private void OnValidate()
        {
            if (!button) TryGetComponent(out button);
        }

        [Inject]
        public void Construct(IGameFactory gameFactory, IPersistenceProgressService persistenceProgressService)
        {
            _persistenceProgressService = persistenceProgressService;
            _gameFactory = gameFactory;
        }

        private void Start()
        {
            button.onClick.AddListener(Restart);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(Restart);
        }

        private void Restart()
        {
        }
    }

}