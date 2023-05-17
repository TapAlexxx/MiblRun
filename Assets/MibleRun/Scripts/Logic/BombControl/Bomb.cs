using DG.Tweening;
using UnityEngine;

namespace Scripts.Logic.BombControl
{

    public class Bomb : MonoBehaviour
    {
        [SerializeField] private ParticleSystem explodeParticle;

        public void Explode()
        {
            explodeParticle.transform.parent = null;
            explodeParticle.Play();
            transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => { gameObject.SetActive(false); });
        }
    }

}