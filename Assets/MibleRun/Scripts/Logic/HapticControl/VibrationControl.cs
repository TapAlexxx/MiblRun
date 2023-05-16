using MoreMountains.NiceVibrations;
using Scripts.Infrastructure.Services.PersistenceProgress;
using UnityEngine;
using Zenject;

namespace Scripts.Logic.HapticControl
{
    public class VibrationControl : MonoBehaviour
    {
        private IPersistenceProgressService _persistenceProgressService;
        private HapticTypes _currentHapticType;

        [Inject]
        public void Construct(IPersistenceProgressService persistenceProgressService) => 
            _persistenceProgressService = persistenceProgressService;

        private void Vibrate()
        {
            _currentHapticType = _persistenceProgressService.PlayerData.ProgressData.VibrationType;
            MMVibrationManager.Haptic(_currentHapticType);
        }
    }

}