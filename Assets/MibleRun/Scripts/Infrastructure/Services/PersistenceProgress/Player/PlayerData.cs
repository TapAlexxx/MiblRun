using System;

namespace Scripts.Infrastructure.Services.PersistenceProgress.Player
{
    [Serializable]
    public class PlayerData
    {
        public PlayerProgressData ProgressData = new PlayerProgressData();
    }
}