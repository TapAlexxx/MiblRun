using Scripts.Infrastructure.Services.Factories.UIFactory;
using Scripts.Infrastructure.Services.Window;
using Scripts.Window;
using UnityEngine;

namespace Scripts.Infrastructure.StateMachine.Game.States
{
    public class LoadMenuState : IPayloadedState<string>, IGameState
    {
        private readonly ILoadingCurtain _loadingCurtain;
        private readonly ISceneLoader _sceneLoader;
        private readonly IWindowService _windowService;
        private readonly IUIFactory _uiFactory;

        public LoadMenuState(ISceneLoader sceneLoader, ILoadingCurtain loadingCurtain, IUIFactory uiFactory,IWindowService windowService)
        {
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _windowService = windowService;
            _uiFactory = uiFactory;
        }
        public void Enter(string payload)
        {
            Time.timeScale = 1f;
            _loadingCurtain.Show();
            _sceneLoader.Load(payload, OnLevelLoad);
        }

        private void OnLevelLoad()
        {
            _uiFactory.CreateUiRoot();
            _windowService.Open(WindowTypeId.Finish);
            
            _loadingCurtain.Hide();
        }

        public void Exit()
        {
        }
    }
}