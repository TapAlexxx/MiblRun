using System;

namespace Scripts.StaticData.Reward
{
    [Serializable]
    public class PointReward
    {
        public int FirstPlace;
        public int SecondPlace;
        public int ThirdPlace;
        public int DefaultPlace;

        public int ForPlace(int carPlace)
        {
            return carPlace switch
            {
                1 => FirstPlace,
                2 => SecondPlace,
                3 => ThirdPlace,
                _ => DefaultPlace,
            };
        }
    }
}