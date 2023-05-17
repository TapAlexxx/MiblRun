using UnityEngine;

namespace Scripts.Logic.BombControl
{

    public class Bomb : MonoBehaviour
    {
        [SerializeField] private ParticleSystem explodeParticle;

        public void Explode()
        {
            explodeParticle.Play();
        }
    }

}