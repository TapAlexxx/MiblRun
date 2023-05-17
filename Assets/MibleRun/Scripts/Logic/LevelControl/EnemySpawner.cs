using System.Collections;
using DG.Tweening;
using Scripts.Logic.BombControl;
using Scripts.Logic.EnemyControl;
using Scripts.Logic.PlayerControl;
using Scripts.Logic.Pool;
using Scripts.StaticClasses;
using Scripts.StaticData.Level;
using UnityEngine;

namespace Scripts.Logic.LevelControl
{

    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyPool enemyPool;

        private float _delayBetweenSpawn;
        private float _spawnRadius;
        private Coroutine _spawnEnemiesCoroutine;
        private PlayerHealth _playerHealth;

        private void OnValidate()
        {
            if (!enemyPool) TryGetComponent(out enemyPool);
        }
        
        public void Initialize(LevelStaticData levelStaticData, Transform player)
        {
            _playerHealth = player.GetComponent<PlayerHealth>();
            _delayBetweenSpawn = levelStaticData.DelayBetweenSpawn;
            enemyPool.Initialize(levelStaticData.EnemyPoolSize);
            _spawnRadius = levelStaticData.SpawnRadius;
        }

        public void SpawnEnemies()
        {
            if (_spawnEnemiesCoroutine != null)
            {
                StopCoroutine(_spawnEnemiesCoroutine);
                _spawnEnemiesCoroutine = null;
            }
            
            _spawnEnemiesCoroutine = StartCoroutine(StartSpawnEnemies());
        }

        private IEnumerator StartSpawnEnemies()
        {
            enemyPool.ResetPool();
            float elapsedTime = 0;
            while(_playerHealth.IsAlive)
            {
                if (elapsedTime > 0)
                {
                    elapsedTime -= Time.deltaTime;
                    yield return null;
                }
                else
                {
                    if (enemyPool.TryGetEnemy(out Enemy bomb))
                    {
                        Vector2 randomPos = GetRandomPosition();
                        bomb.transform.localPosition = new Vector3(randomPos.x, Constants.EnemyDefaultY,randomPos.y);
                        bomb.gameObject.SetActive(true);
                    }
                    else
                    {
                        break;
                    }
                    elapsedTime = _delayBetweenSpawn;
                }
            }
        }

        private Vector2 GetRandomPosition()
        {
            Vector2 randomPos = Random.insideUnitCircle * _spawnRadius;
            while (randomPos.sqrMagnitude < 3)
            {
                randomPos = Random.insideUnitCircle * _spawnRadius;
            }

            return randomPos;
        }
        
    }

}