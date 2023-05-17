using System.Collections.Generic;
using System.Linq;
using Scripts.StaticData;
using Scripts.StaticData.Level;
using Scripts.StaticData.Window;
using Scripts.Window;
using UnityEngine;

namespace Scripts.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string GameConfigPath = "StaticData/GameConfig";
        private const string WindowsStaticDataPath = "StaticData/WindowsStaticData";
        private const string LevelStaticDataPath = "StaticData/LevelStaticData/LevelData";
        private const string PlayerStaticDataPath = "StaticData/PlayerStaticData/PlayerStaticData";

        private GameStaticData _gameStaticData;
        private Dictionary<WindowTypeId, WindowConfig> _windowConfigs;
        private LevelStaticData _levelStaticData;
        private PlayerStaticData _playerStaticData;

        public void LoadData()
        {
            _gameStaticData = Resources
                .Load<GameStaticData>(GameConfigPath);

            _windowConfigs = Resources
                .Load<WindowStaticData>(WindowsStaticDataPath)
                .Configs
                .ToDictionary(x => x.WindowTypeId, x => x);

            _levelStaticData = Resources
                .Load<LevelStaticData>(LevelStaticDataPath);
            
            _playerStaticData = Resources
                .Load<PlayerStaticData>(PlayerStaticDataPath);
        }

        public GameStaticData GameConfig() =>
            _gameStaticData;

        public WindowConfig ForWindow(WindowTypeId windowTypeId) => 
            _windowConfigs[windowTypeId];

        public PlayerStaticData PlayerStaticData() => 
            _playerStaticData;

        public LevelStaticData GetLevelStaticData() => 
            _levelStaticData;
    }
}