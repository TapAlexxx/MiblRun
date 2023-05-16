using System;

namespace Scripts.StaticData.Reward
{
    [Serializable]
    public class LevelReward
    {
        public CoinReward CoinReward;
        public PointReward PointReward;
        public PointReward GiftReward;
    }
}