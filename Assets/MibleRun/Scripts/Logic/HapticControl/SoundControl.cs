using UnityEngine;
using Zenject;

namespace Scripts.Logic.HapticControl
{
    public class SoundControl : MonoBehaviour
    {
        private ISoundEffectService _soundEffectService;

        [Inject]
        public void Construct(ISoundEffectService soundEffectService)
        {
            _soundEffectService = soundEffectService;
        }
        
        private void Pop()
        {
            _soundEffectService.Pop();
        }

        private void Win()
        {
            _soundEffectService.Win();
        }
    }

}