using System.Collections;
using Scripts.Infrastructure.Services.Factories.Game;
using Scripts.Infrastructure.Services.PersistenceProgress;
using Scripts.Infrastructure.Services.SaveLoad;
using Scripts.Logic.PlayerControl;
using TMPro;
using UnityEngine;
using Zenject;

namespace Scripts.Logic.Hud
{

    public class TimeAliveCounter : MonoBehaviour
    {
        [SerializeField] private GameStarter gameStarter;
        [SerializeField] private TMP_Text timerText;
        
        private Coroutine _countTimeCoroutine;
        private IPersistenceProgressService _persistenceProgressService;
        private ISaveLoadService _saveLoadService;
        private IGameFactory _gameFactory;
        private PlayerHealth _playerHealth;
        private TimeConverter _timeConverter;
        
        public float CurrentTime { get; private set; }

        [Inject]
        public void Construct(IPersistenceProgressService persistenceProgressService, ISaveLoadService saveLoadService,
            IGameFactory gameFactory)
        {
            _persistenceProgressService = persistenceProgressService;
            _saveLoadService = saveLoadService;
            _gameFactory = gameFactory;
            _playerHealth = _gameFactory.Player.GetComponent<PlayerHealth>();
        }
        
        private void Start()
        {
            gameStarter.GameStarted += StartCount;
            Initialize();
        }

        private void OnDestroy()
        {
            gameStarter.GameStarted -= StartCount;
        }

        private void Initialize()
        {
            _timeConverter = new TimeConverter();
            timerText.text = _timeConverter.ConvertToText(0);
        }

        private void StartCount()
        {
            if (_countTimeCoroutine != null)
            {
                TrySaveBest();
                StopCoroutine(_countTimeCoroutine);
                _countTimeCoroutine = null;
            }
            _countTimeCoroutine = StartCoroutine(Count());
        }

        private IEnumerator Count()
        {
            CurrentTime = 0;
            while (_playerHealth.IsAlive)
            {
                CurrentTime += Time.deltaTime;
                timerText.text = _timeConverter.ConvertToText(CurrentTime);
                yield return null;
            }
            
            TrySaveBest();
        }

        private void TrySaveBest()
        {
            if (CurrentTime > _persistenceProgressService.PlayerData.ProgressData.BestTime)
            {
                _persistenceProgressService.PlayerData.ProgressData.BestTime = CurrentTime;
            }
            _persistenceProgressService.PlayerData.ProgressData.CurrentTime = CurrentTime;
            _saveLoadService.SaveProgress();
        }
    }

}