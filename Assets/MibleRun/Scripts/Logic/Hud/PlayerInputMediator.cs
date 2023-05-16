using System;
using Scripts.Infrastructure.Services.Factories.Game;
using Scripts.Logic.PlayerControl.PlayerInput;
using UnityEngine;
using Zenject;

namespace Scripts.Logic.Hud
{

    public class PlayerInputMediator : MonoBehaviour
    {
        [SerializeField] private JoystickInput joystickInput;
        
        private IGameFactory _gameFactory;
        private PlayerMovement _playerMovement;

        private void OnValidate()
        {
            if (!joystickInput) TryGetComponent(out joystickInput);
        }

        [Inject]
        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
            _playerMovement = _gameFactory.Player.GetComponent<PlayerMovement>();
        }
        
        private void Start()
        {
            joystickInput.StartedDrag += ResetTargetDirection;
            joystickInput.EndedDrag += ResetTargetDirection;
        }

        private void OnDestroy()
        {
            joystickInput.StartedDrag -= ResetTargetDirection;
            joystickInput.EndedDrag -= ResetTargetDirection;
        }

        private void Update()
        {
            if (joystickInput.IsOnDrag)
            {
                _playerMovement.SetTargetDirection(joystickInput.NormalizedDirection);
            }
        }

        private void ResetTargetDirection()
        {
            _playerMovement.SetTargetDirection(Vector3.zero);
        }
    }

}