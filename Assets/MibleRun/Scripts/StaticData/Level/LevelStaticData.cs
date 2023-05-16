using UnityEngine;

namespace Scripts.StaticData.Level
{
    [CreateAssetMenu(menuName = "StaticData/Level", fileName = "LevelStaticData", order = 0)]
    public class LevelStaticData : ScriptableObject
    {
        public float TimeBetweenSpawn;
    }

}