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

        private GameStaticData _gameStaticData;
        private Dictionary<WindowTypeId, WindowConfig> _windowConfigs;
        private LevelStaticData _levelStaticData;

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
        }

        public GameStaticData GameConfig() =>
            _gameStaticData;

        public WindowConfig ForWindow(WindowTypeId windowTypeId) => 
            _windowConfigs[windowTypeId];

        public LevelStaticData GetLevelStaticData() => 
            _levelStaticData;
    }
}