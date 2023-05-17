using System.Collections.Generic;
using System.Linq;
using Scripts.Infrastructure.Services.Factories.Game;
using Scripts.Logic.EnemyControl;
using UnityEngine;
using Zenject;

namespace Scripts.Logic.Pool
{

    public class EnemyPool : MonoBehaviour
    {
        private int _poolSize;

        private List<Enemy> _pool;
        private IGameFactory _gameFactory;

        [Inject]
        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }
        
        public void Initialize(int poolSize)
        {
            _poolSize = poolSize;
            FillPool();
        }

        private void FillPool()
        {
            _pool = new List<Enemy>();
            for (int i = 0; i < _poolSize; i++)
            {
                Enemy enemy = _gameFactory.CreateEnemy(transform);
                _pool.Add(enemy);
                enemy.gameObject.SetActive(false);
            }
        }

        public bool TryGetEnemy(out Enemy enemy)
        {
            enemy = _pool.FirstOrDefault(x => x.gameObject.activeSelf == false);
            return enemy != null;
        }

        public void ResetPool()
        {
            foreach (Enemy enemy in _pool) 
                enemy.gameObject.SetActive(false);
        }
    }

}