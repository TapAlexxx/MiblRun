using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Scripts.Logic.BombControl;
using UnityEngine;

namespace Scripts.Logic.Pool
{

    public class BombPool : MonoBehaviour
    {
        private List<Bomb> _pool;
        private Bomb _prefab;
        private int _poolSize;

        public void Initialize(Bomb prefab, int poolSize)
        {
            _poolSize = poolSize;
            _prefab = prefab;
            _pool = new List<Bomb>();
            for (int i = 0; i < poolSize; i++)
            {
                Bomb bomb = Instantiate(prefab, transform).GetComponent<Bomb>();
                _pool.Add(bomb);
                bomb.gameObject.SetActive(false);
            }    
        }
        
        public bool TryGetBomb(out Bomb bomb)
        {
            bomb = _pool.FirstOrDefault(x => x.gameObject.activeSelf == false);
            return bomb != null;
        }

        public void ResetPool()
        {
            foreach (Bomb bomb in _pool)
            {
                bomb.transform
                    .DOScale(0, 0.3f)
                    .OnComplete(() => { bomb.gameObject.SetActive(false); });
            }
        }
    }

}