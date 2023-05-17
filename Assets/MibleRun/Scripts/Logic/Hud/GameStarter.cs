using System;
using System.Collections;
using DG.Tweening;
using Scripts.Infrastructure.Services.Factories.Game;
using Scripts.Logic.LevelControl;
using Scripts.Logic.PlayerControl;
using Scripts.StaticClasses;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Scripts.Logic.Hud
{

    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Text text;

        private PlayerStateControl _stateControl;
        private EnemySpawner _enemySpawner;
        private Coroutine _startCoroutine;

        public event Action GameStarted;
        public event Action NeedToSwitchCamera;

        [Inject]
        public void Construct(IGameFactory gameFactory)
        {
            _stateControl = gameFactory.Player.GetComponent<PlayerStateControl>();
            _enemySpawner = gameFactory.EnemySpawner;
        }

        private void Start()
        {
            button.onClick.AddListener(EnterMoveState);
            text.DOFade(1f, 1f).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDestroy() => 
            button.onClick.RemoveListener(EnterMoveState);

        private void EnterMoveState()
        {
            if (_startCoroutine != null)
                return;
            _startCoroutine = StartCoroutine(StartGame());
        }

        private IEnumerator StartGame()
        {
            text.gameObject.SetActive(false);
            NeedToSwitchCamera?.Invoke();
            yield return new WaitForSeconds(Constants.BlendToDefaultTime);
            button.gameObject.SetActive(false);
            _stateControl.EnterMoveState();
            _enemySpawner.SpawnEnemies();
            GameStarted?.Invoke();
        }
    }

}