using System;
using Scripts.Logic.BombControl;
using Scripts.Logic.EnemyControl;
using UnityEngine;

namespace Scripts.Logic.PlayerControl
{

    public class ExplosionObserver : MonoBehaviour
    {
        public event Action Exploded;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.TryGetComponent(out Enemy enemy))
            {
                enemy.Explode();
                Exploded?.Invoke();
            }
            
            if (collision.collider.TryGetComponent(out Bomb bomb))
            {
                bomb.Explode();
                Exploded?.Invoke();
            }
        }
    }

}