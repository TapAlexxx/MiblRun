using Scripts.Infrastructure.Services.PersistenceProgress;
using Scripts.Infrastructure.Services.PersistenceProgress.Player;
using Scripts.Infrastructure.Services.SaveLoad;

namespace Scripts.Infrastructure.StateMachine.Game.States
{
    public class LoadProgressState : IPayloadedState<string>, IGameState
    {
        private readonly IStateMachine<IGameState> _stateMachine;
        private readonly IPersistenceProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(IStateMachine<IGameState> stateMachine,
            IPersistenceProgressService progressService, ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _stateMachine = stateMachine;
            _progressService = progressService;
        }

        public void Enter(string payload)
        {
            LoadOrCreatePlayerData();
            _stateMachine.Enter<LoadLevelState, string>(payload);
        }

        public void Exit()
        {
            
        }

        private PlayerData LoadOrCreatePlayerData() =>
            _progressService.PlayerData =
                _saveLoadService.LoadProgress()
                ?? CreateNew();

        private PlayerData CreateNew() => 
            new PlayerData();
    }
}