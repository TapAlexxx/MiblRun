using System;
using DG.Tweening;
using Scripts.Infrastructure.Services.Factories.Game;
using Scripts.Logic.LevelControl;
using Scripts.Logic.PlayerControl;
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

        [Inject]
        public void Construct(IGameFactory gameFactory)
        {
            _stateControl = gameFactory.Player.GetComponent<PlayerStateControl>();
            _enemySpawner = gameFactory.EnemySpawner;
        }

        private void Start()
        {
            button.onClick.AddListener(EnterRideState);
            text.DOFade(1f, 1f).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnDestroy() => 
            button.onClick.RemoveListener(EnterRideState);

        private void EnterRideState()
        {
            text.gameObject.SetActive(false);
            button.gameObject.SetActive(false);
            _stateControl.EnterMoveState();
            _enemySpawner.SpawnEnemies();
        }
    }

}