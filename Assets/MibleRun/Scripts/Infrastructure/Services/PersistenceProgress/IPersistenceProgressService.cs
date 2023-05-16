using Scripts.Infrastructure.Services.PersistenceProgress.Player;

namespace Scripts.Infrastructure.Services.PersistenceProgress
{
    public interface IPersistenceProgressService
    {
        PlayerData PlayerData { get; set; }
    }
}