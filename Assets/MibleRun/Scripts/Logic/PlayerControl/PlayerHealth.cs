using System;
using UnityEngine;

namespace Scripts.Logic.PlayerControl
{

    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private ExplosionObserver explosionObserver;
        
        public bool IsAlive { get; private set; }

        private void OnValidate()
        {
            if (!explosionObserver) TryGetComponent(out explosionObserver);
        }

        private void Start()
        {
            explosionObserver.Exploded += Die;
        }

        private void OnDestroy()
        {
            explosionObserver.Exploded -= Die;
        }

        public void Revive()
        {
            IsAlive = true;
        }

        private void Die()
        {
            IsAlive = false;
        }
    }

}