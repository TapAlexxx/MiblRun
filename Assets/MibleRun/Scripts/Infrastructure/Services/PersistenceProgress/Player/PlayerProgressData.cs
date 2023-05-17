using System;

namespace Scripts.Infrastructure.Services.PersistenceProgress.Player
{
    [Serializable]
    public class PlayerProgressData
    {
        public float BestTime = 0;
        public float CurrentTime = 0;
        
        public Action CurrentTimeChanged;
        public Action BestTimeChanged;

        public void UpdateCurrentTime(float time)
        {
            CurrentTime = time;
            CurrentTimeChanged?.Invoke();
        }

        public void UpdateBest(float currentTime)
        {
            BestTime = currentTime;
            BestTimeChanged?.Invoke();
        }
    }
}