using System.Collections.Generic;
using System.Linq;
using Scripts.Logic.EnemyControl;
using UnityEngine;

namespace Scripts.Logic.Pool
{

    public class EnemyPool : MonoBehaviour
    {
        [SerializeField] private Enemy prefab;
        [SerializeField] private int poolSize;

        private List<Enemy> _pool;

        public void Initialize()
        {
            _pool = new List<Enemy>();
            for (int i = 0; i < poolSize; i++)
            {
                Enemy bomb = Instantiate(prefab, transform).GetComponent<Enemy>();
                _pool.Add(bomb);
                bomb.gameObject.SetActive(false);
            }    
        }
        
        public bool TryGetEnemy(out Enemy enemy)
        {
            enemy = _pool.FirstOrDefault(x => x.gameObject.activeSelf == false);
            return enemy != null;
        }
    }

}