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

        private void OnValidate()
        {
            if (!explosionObserver) TryGetComponent(out explosionObserver);
        }

        [Inject]
        public void Construct(IWindowService windowService)
        {
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
            _windowService.Open(WindowTypeId.Finish);
            _enteredFinish = true;
        }
    }

}