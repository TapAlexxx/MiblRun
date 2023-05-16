using Scripts.Infrastructure.StateMachine.Game.States;
using Zenject;

namespace Scripts.Infrastructure.StateMachine.Game
{
    public class GameStateMachine : StateMachine<IGameState>, ITickable
    {
        public GameStateMachine(GameStateFactory gameStateFactory) : base(gameStateFactory)
        {
        }

        public void Tick()
        {
            Update();
        }
    }
}