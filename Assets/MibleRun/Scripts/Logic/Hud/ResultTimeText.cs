using System;
using Scripts.Infrastructure.Services.PersistenceProgress;
using TMPro;
using UnityEngine;
using Zenject;

namespace Scripts.Logic.Hud
{

    public class ResultTimeText : MonoBehaviour
    {
        [SerializeField] private TMP_Text bestText;
        [SerializeField] private TMP_Text currentText;
        
        private IPersistenceProgressService _persistenceProgressService;
        private TimeConverter _timeConverter;

        [Inject]
        public void Construct(IPersistenceProgressService persistenceProgressService)
        {
            _persistenceProgressService = persistenceProgressService;
        }
        
        private void Start()
        {
            _timeConverter = new TimeConverter();

            RefreshText();
            _persistenceProgressService.PlayerData.ProgressData.BestTimeChanged += RefreshText;
            _persistenceProgressService.PlayerData.ProgressData.CurrentTimeChanged += RefreshText;
        }

        private void OnDestroy()
        {
            _persistenceProgressService.PlayerData.ProgressData.BestTimeChanged -= RefreshText;
            _persistenceProgressService.PlayerData.ProgressData.CurrentTimeChanged -= RefreshText;
        }

        private void RefreshText()
        {
            float currentBest = _persistenceProgressService.PlayerData.ProgressData.BestTime;
            float current = _persistenceProgressService.PlayerData.ProgressData.CurrentTime;

            bestText.text = current >= currentBest 
                ? "NEW BEST!" 
                : $"BEST: {_timeConverter.ConvertToText(currentBest)}";
            
            currentText.text = $"YOU WERE ALIVE: {_timeConverter.ConvertToText(current)}";
        }
    }

}