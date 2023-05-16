using Scripts.Infrastructure.Services.PersistenceProgress.Player;

namespace Scripts.Infrastructure.Services.PersistenceProgress
{
    public class PersistenceProgressService : IPersistenceProgressService
    {
        public PlayerData PlayerData { get; set; }
    }
}