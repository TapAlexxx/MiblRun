using System;
using Scripts.Logic.BombControl;
using Scripts.Logic.EnemyControl;
using UnityEngine;

namespace Scripts.Logic.PlayerControl
{

    public class ExplosionObserver : MonoBehaviour
    {
        private bool _exploded;
        public event Action Exploded;

        private void OnCollisionEnter(Collision collision)
        {
            if(_exploded)
                return;

            if (collision.collider.TryGetComponent(out Bomb bomb))
            {
                bomb.Explode();
                Exploded?.Invoke();
                _exploded = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(_exploded)
                return;
            
            if (other.TryGetComponent(out Enemy enemy))
            {
                enemy.Explode();
                Exploded?.Invoke();
                _exploded = true;
            }
        }
    }

}