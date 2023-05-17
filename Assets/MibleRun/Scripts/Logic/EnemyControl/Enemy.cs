using System;
using UnityEngine;

namespace Scripts.Logic.EnemyControl
{

    public class Enemy : MonoBehaviour
    {
        [SerializeField] private EnemyMover enemyMover;
        [SerializeField] private Renderer enemyRenderer;
        [SerializeField] private Color color;
        [SerializeField] private ParticleSystem explodeParticle;
        
        private void OnValidate()
        {
            if (!enemyMover) TryGetComponent(out enemyMover);
        }

        public void Initialize(Transform player)
        {
            enemyMover.Initialize(player);
            enemyRenderer.materials[0].color = color;
        }

        public void Explode()
        {
            explodeParticle.Play();
        }
    }

}