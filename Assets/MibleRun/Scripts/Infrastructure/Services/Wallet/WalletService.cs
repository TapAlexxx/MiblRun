using System;
using Scripts.Infrastructure.Services.PersistenceProgress;
using Scripts.Infrastructure.Services.PersistenceProgress.Player;
using Scripts.Infrastructure.Services.SaveLoad;

namespace Scripts.Infrastructure.Services.Wallet
{

    public class WalletService : IWalletService
    {
        private readonly IPersistenceProgressService _persistenceProgressService;
        private ISaveLoadService _saveLoadService;

        public WalletService(IPersistenceProgressService persistenceProgressService, ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _persistenceProgressService = persistenceProgressService;
        }

        private PlayerProgressData PlayerProgress => _persistenceProgressService.PlayerData.ProgressData;

        public void AddCoins(int amount)
        {
            if (amount < 1)
                throw new ArgumentOutOfRangeException(nameof(amount) + $"can't be less 1. You value {amount}");
            
            PlayerProgress.AddCoins(amount);
            _saveLoadService.SaveProgress();
        }
        
        public void WithdrawCoins(int amount)
        {
            if (amount < 1)
                throw new ArgumentOutOfRangeException(nameof(amount) + $"can't be less 1. You value {amount}");
            
            if (!CanWithdrawCoins(amount))
                throw new InvalidOperationException(nameof(amount) + $"can't be more than player have. Player have {PlayerProgress.Сoins} you try withdraw {amount}");

            PlayerProgress.WithdrawCoins(amount);
            _saveLoadService.SaveProgress();
        }

        public bool CanWithdrawCoins(int amount)
        {
            if (amount < 1)
                throw new ArgumentOutOfRangeException(nameof(amount) + $"can't be less 1. You value {amount}");
            
            return PlayerProgress.Сoins >= amount;
        }
    }

}