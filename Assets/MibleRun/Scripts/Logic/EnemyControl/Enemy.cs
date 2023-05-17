using System;
using DG.Tweening;
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
            explodeParticle.transform.SetParent(null);
            explodeParticle.Play();
            transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => { gameObject.SetActive(false); });
        }

        public void Activate()
        {
            enemyMover.StartMove();
        }
    }

}