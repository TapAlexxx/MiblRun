using Scripts.Infrastructure.Services.PersistenceProgress;
using UnityEngine;
using Zenject;

namespace Scripts.Logic.WindowControls
{

    public class LevelText : MonoBehaviour
    {
        [SerializeField] private LevelTextView currentLevelText;
        [SerializeField] private LevelTextView prevLevelText;
        [SerializeField] private LevelTextView nextLevelText;
        
        private IPersistenceProgressService _persistenceProgressService;

        [Inject]
        public void Construct(IPersistenceProgressService persistenceProgressService)
        {
            _persistenceProgressService = persistenceProgressService;
        }
        
        private void OnEnable()
        {
            RefreshText();
            _persistenceProgressService.PlayerData.ProgressData.LevelChanged += RefreshText;
        }

        private void OnDisable()
        {
            if(_persistenceProgressService == null)
                return;
            
            _persistenceProgressService.PlayerData.ProgressData.LevelChanged -= RefreshText;
        }

        private void RefreshText()
        {
            int level = _persistenceProgressService.PlayerData.ProgressData.CurrentLevel;
            int prevLevel = level - 1;
            int nextLevel = level + 1;
            
            bool isPrevBoss = prevLevel % 5 == 0;
            bool isCurrentBoss = level % 5 == 0;
            bool isNextBoss = nextLevel % 5 == 0;
            
            prevLevelText.ViewLevel(prevLevel, isPrevBoss);
            currentLevelText.ViewLevel(level, isCurrentBoss);
            nextLevelText.ViewLevel(nextLevel, isNextBoss);
            
            prevLevelText.gameObject.SetActive(prevLevel > 0);
        }
    }

}