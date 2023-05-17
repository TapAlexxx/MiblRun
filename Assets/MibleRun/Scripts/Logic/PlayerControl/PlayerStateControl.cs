using Scripts.Infrastructure.Services.Factories.Game;
using Scripts.Infrastructure.Services.Window;
using Scripts.Logic.Unit;
using Scripts.Window;
using UnityEngine;
using Zenject;

namespace Scripts.Logic.PlayerControl
{

    public class PlayerStateControl : MonoBehaviour
    {
        [SerializeField] private ExplosionObserver explosionObserver;
        [SerializeField] private UnitMovement unitMovement;
        
        private IWindowService _windowService;
        private bool _enteredFinish;
        private IGameFactory _gameFactory;

        private void OnValidate()
        {
            if (!explosionObserver) TryGetComponent(out explosionObserver);
            if (!unitMovement) TryGetComponent(out unitMovement);
        }

        [Inject]
        public void Construct(IWindowService windowService, IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _windowService = windowService;
        }
        
        private void Start()
        {
            explosionObserver.Exploded += EnterFinishState;
        }

        private void OnDestroy()
        {
            explosionObserver.Exploded -= EnterFinishState;
        }

        public void EnterMoveState()
        {
            unitMovement.Activate();
        }
        
        private void EnterFinishState()
        {
            if(_enteredFinish)
                return;
            
            unitMovement.Disable();
            _gameFactory.GameHud.SetActive(false);
            _windowService.Open(WindowTypeId.Finish);
            _enteredFinish = true;
        }
    }

}