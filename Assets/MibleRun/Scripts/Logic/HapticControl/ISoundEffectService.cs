namespace Scripts.Logic.HapticControl
{

    public interface ISoundEffectService
    {
        void Pop();
        void Win();
        void Mute();
        void On();
        void Refresh(bool currentSoundSettings);
    }

}