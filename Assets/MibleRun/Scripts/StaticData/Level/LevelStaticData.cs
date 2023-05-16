using UnityEngine;

namespace Scripts.StaticData.Level
{
    [CreateAssetMenu(menuName = "StaticData/Level", fileName = "LevelData", order = 0)]
    public class LevelStaticData : ScriptableObject
    {
        public float TimeBetweenSpawn;
    }

}