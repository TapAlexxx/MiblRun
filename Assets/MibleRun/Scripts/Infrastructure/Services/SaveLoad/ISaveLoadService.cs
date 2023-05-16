using Scripts.Extensions;
using Scripts.Infrastructure.Services.PersistenceProgress;
using Scripts.Infrastructure.Services.PersistenceProgress.Player;
using UnityEngine;

namespace Scripts.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        PlayerData LoadProgress();
        void SaveProgress();
        void ResetProgress();
    }
    
    public class SaveLoadService : ISaveLoadService
    {
        private const string PlayerProgressKey = "PlayerProgress";
        
        private readonly IPersistenceProgressService _persistenceProgressService;
        
        public SaveLoadService(IPersistenceProgressService persistenceProgressService)
        {
            _persistenceProgressService = persistenceProgressService;
        }

        public void SaveProgress() => 
            PlayerPrefs.SetString(PlayerProgressKey, _persistenceProgressService.PlayerData.ToJson());

        public PlayerData LoadProgress() => 
            PlayerPrefs.GetString(PlayerProgressKey)?
                .ToDeserialize<PlayerData>();

        public void ResetProgress() => PlayerPrefs.DeleteKey(PlayerProgressKey);
    }
}