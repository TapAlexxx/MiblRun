using Scripts.Logic.BombControl;
using Scripts.Logic.EnemyControl;
using UnityEngine;

namespace Scripts.StaticData.Level
{
    [CreateAssetMenu(menuName = "StaticData/Level", fileName = "LevelStaticData", order = 0)]
    public class LevelStaticData : ScriptableObject
    {
        public float SpawnRadius = 20f;

        [Space(20), Header("Bombs")]
        public GameObject BombSpawner;
        public Bomb BombPrefab;
        public int BombPoolSize;
        public int BombCount = 20;

        [Space(20), Header("Enemies")] 
        public GameObject EnemySpawner;
        public Enemy EnemyPrefab;
        public int EnemyPoolSize;
        public float DelayBetweenSpawn = 1f;
        public float EnemyMoveSpeed = 8f;
        public float EnemyRotationSpeed = 10f;
    }

}