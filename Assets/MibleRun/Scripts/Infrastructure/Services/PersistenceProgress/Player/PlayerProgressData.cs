using System;
using MoreMountains.NiceVibrations;

namespace Scripts.Infrastructure.Services.PersistenceProgress.Player
{
    [Serializable]
    public class PlayerProgressData
    {
        public int Сoins;
        public int CurrentLevel = 1;

        public HapticTypes VibrationType = HapticTypes.LightImpact;
        public bool IsSoundOn = true;
        
        public Action CoinsChanged;
        public Action LevelChanged;
        public Action VibrationChanged;

        public void AddCoins(int amount)
        {
            Сoins += amount;
            CoinsChanged?.Invoke();
        }

        public void WithdrawCoins(int amount)
        {
            Сoins -= amount;
            CoinsChanged?.Invoke();
        }

        public void IncreaseLevel()
        {
            CurrentLevel++;
            LevelChanged?.Invoke();
        }

        public void SwitchVibrationType(HapticTypes targetVibrationType)
        {
            VibrationType = targetVibrationType;
            VibrationChanged?.Invoke();
        }

        public void SoundOn()
        {
            IsSoundOn = true;
        }

        public void SoundOff()
        {
            IsSoundOn = false;
        }
    }
}