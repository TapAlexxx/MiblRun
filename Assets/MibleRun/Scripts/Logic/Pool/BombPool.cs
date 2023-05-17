using System.Collections.Generic;
using System.Linq;
using Scripts.Logic.BombControl;
using UnityEngine;

namespace Scripts.Logic.Pool
{

    public class BombPool : MonoBehaviour
    {
        [SerializeField] private Bomb prefab;
        [SerializeField] private int poolSize;

        private List<Bomb> _pool;

        public void Initialize()
        {
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
    }

}