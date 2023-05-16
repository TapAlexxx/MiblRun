using MoreMountains.NiceVibrations;
using Scripts.Infrastructure.Services.PersistenceProgress;
using Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Logic.WindowControls
{

    public class VibrationSettingsButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private VibrationSettingsView view; 
        
        private ISaveLoadService _saveLoadService;
        private IPersistenceProgressService _persistenceProgressService;
        private HapticTypes _currentVibrationType;

        [Inject]
        public void Construct(IPersistenceProgressService persistenceProgressService, ISaveLoadService saveLoadService)
        {
            _persistenceProgressService = persistenceProgressService;
            _saveLoadService = saveLoadService;
        }
        
        private void Start()
        {
            InitializeFromSave();
            button.onClick.AddListener(SwitchVibrations);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(SwitchVibrations);
        }

        private void SwitchVibrations()
        {
            HapticTypes targetVibrationType = _currentVibrationType switch
            {
                HapticTypes.None => HapticTypes.LightImpact,
                HapticTypes.LightImpact => HapticTypes.SoftImpact,
                HapticTypes.SoftImpact => HapticTypes.None,
                _ => HapticTypes.None
            };

            _currentVibrationType = targetVibrationType;
            _persistenceProgressService.PlayerData.ProgressData.SwitchVibrationType(targetVibrationType);
            view.RefreshView(_currentVibrationType);
            _saveLoadService.SaveProgress();
        }

        private void InitializeFromSave()
        {
            _currentVibrationType = _persistenceProgressService.PlayerData.ProgressData.VibrationType;
            view.RefreshView(_currentVibrationType);
            _saveLoadService.SaveProgress();
        }
    }

}