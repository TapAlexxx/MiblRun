using Scripts.Infrastructure.Services.Factories.Game;
using Scripts.Infrastructure.Services.PersistenceProgress;
using Scripts.Infrastructure.StateMachine;
using Scripts.Infrastructure.StateMachine.Game.States;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Logic.WindowControls
{

    public class RestartLevelButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        
        private IStateMachine<IGameState> _gameStateMachine;

        private void OnValidate()
        {
            if (!button) TryGetComponent(out button);
        }

        [Inject]
        public void Construct(IStateMachine<IGameState> gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
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
            _gameStateMachine.Enter<LoadLevelState, string>(SceneManager.GetActiveScene().name);
        }
    }

}