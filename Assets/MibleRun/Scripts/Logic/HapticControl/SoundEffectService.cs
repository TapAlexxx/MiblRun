using UnityEngine;

namespace Scripts.Logic.HapticControl
{

    public class SoundEffectService : MonoBehaviour, ISoundEffectService
    {
        [SerializeField] private AudioSource popAudioSource;
        [SerializeField] private AudioSource winAudioSource;

        private bool _isSoundOn;

        public void Mute() => 
            _isSoundOn = false;

        public void On() => 
            _isSoundOn = true;

        public void Refresh(bool currentSoundSettings) => 
            _isSoundOn = currentSoundSettings;

        public void Win()
        {
            if(!_isSoundOn)
                return;
            
            if(winAudioSource.isPlaying)
                return;
            
            winAudioSource.Play();
        }

        public void Pop()
        {
            if(!_isSoundOn)
                return;
            
            if(popAudioSource.isPlaying)
                return;
            
            popAudioSource.Play();
        }
    }

}