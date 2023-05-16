using System.Collections.Generic;
using System.Linq;
using Scripts.Logic.MonsterControl;
using UnityEngine;

namespace Scripts.Logic.Pool
{
    public class MonsterPool : MonoBehaviour
    {
        [SerializeField] private int cellPoolSize = 100;
        [SerializeField] private Monster monsterPrefab;
        
        private List<Monster> _monsters;

        private void Awake()
        {
            InitializeCellPool();
        }

        private void InitializeCellPool()
        {
            _monsters = new List<Monster>();
            for (int i = 0; i < cellPoolSize; i++)
            {
                Monster monster = Instantiate(monsterPrefab, transform);
                monster.gameObject.SetActive(false);
                _monsters.Add(monster);
            }
        }

        public void ResetPool()
        {
            foreach (Monster monster in _monsters) 
                monster.gameObject.SetActive(false);
        }

        public bool TryGetMonster(out Monster monster)
        {
            monster = _monsters.FirstOrDefault(x => x.gameObject.activeSelf == false);
            return monster != null;
        }
    }
}