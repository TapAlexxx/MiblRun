using Scripts.Infrastructure.Services.PersistenceProgress;
using Scripts.Infrastructure.Services.SaveLoad;
using Scripts.Logic.HapticControl;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Logic.WindowControls
{

    public class SoundSettingsButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private SoundSettingsView view; 

        private IPersistenceProgressService _persistenceProgressService;
        private ISaveLoadService _saveLoadService;
        private bool _isSoundOn;
        private ISoundEffectService _soundEffectService;

        [Inject]
        public void Construct(IPersistenceProgressService persistenceProgressService,
            ISaveLoadService saveLoadService, ISoundEffectService soundEffectService)
        {
            _soundEffectService = soundEffectService;
            _persistenceProgressService = persistenceProgressService;
            _saveLoadService = saveLoadService;
        }
        
        private void Start()
        {
            InitializeFromSave();
            button.onClick.AddListener(SwitchSound);
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(SwitchSound);
        }

        private void SwitchSound()
        {
            _isSoundOn = !_isSoundOn;
            _persistenceProgressService.PlayerData.ProgressData.IsSoundOn = _isSoundOn;
            view.RefreshView(_isSoundOn);
            _soundEffectService.Refresh(_isSoundOn);
            _saveLoadService.SaveProgress();
        }

        private void InitializeFromSave()
        {
            _isSoundOn = _persistenceProgressService.PlayerData.ProgressData.IsSoundOn;
            view.RefreshView(_isSoundOn);
            _soundEffectService.Refresh(_isSoundOn);
            _saveLoadService.SaveProgress();
        }
    }

}