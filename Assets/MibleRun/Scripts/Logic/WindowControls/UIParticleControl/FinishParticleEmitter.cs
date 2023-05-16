using UnityEngine;

namespace Scripts.Logic.WindowControls.UIParticleControl
{

    public class FinishParticleEmitter : MonoBehaviour
    {
		[SerializeField] private ParticleSystem leftParticle;
		[SerializeField] private ParticleSystem rightParticle;

		private void EmmitOnFinish()
		{
			leftParticle.Play();
			rightParticle.Play();
		}
    }

}