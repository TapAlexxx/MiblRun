using Scripts.Infrastructure.Services.Factories.Game;
using UnityEngine;

namespace Scripts.Infrastructure.StateMachine.Game.States
{
    public class GameLoopState : IState, IGameState, IUpdatable
    {
        private IGameFactory _gameFactory;

        public GameLoopState(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void Enter()
        {
            Debug.Log("GameLoop");
        }

        public void Update()
        {
            
        }

        public void Exit()
        {
            
        }
    }
}