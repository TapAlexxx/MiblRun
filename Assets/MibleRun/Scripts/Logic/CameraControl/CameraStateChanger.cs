using System;
using Cinemachine;
using Scripts.Logic.Hud;
using Scripts.Logic.PlayerControl;
using UnityEngine;

namespace Scripts.Logic.CameraControl
{

    public class CameraStateChanger : MonoBehaviour
    {
        [SerializeField] private GameObject[] virtualCamerasObjects;
        [SerializeField] private CinemachineVirtualCamera[] virtualCameras;

        private int[] _virtualCamerasID;

        private Transform _target;
        private GameStarter _gameStarter;
        private ExplosionObserver _playerExplosionObserver;

        public void Construct(GameStarter gameStarter, ExplosionObserver playerExplosionObserver)
        {
            _playerExplosionObserver = playerExplosionObserver;
            _gameStarter = gameStarter;
            _gameStarter.GameStarted += SwitchToDefault;
            _playerExplosionObserver.Exploded += SwitchToFinish;
        }

        private void OnDestroy()
        {
            if(_gameStarter)
                _gameStarter.GameStarted -= SwitchToDefault;
            if(_playerExplosionObserver)
                _playerExplosionObserver.Exploded -= SwitchToFinish;
        }

        private void SwitchToDefault() => 
            SwitchTo(CameraViewState.Default);

        private void SwitchToFinish() => 
            SwitchTo(CameraViewState.Finish);

        public void Initialize(Transform target)
        {
            _target = target;
            for (int i = 0; i < _virtualCamerasID.Length; i++)
            {
                virtualCameras[i].Follow = target;
                virtualCameras[i].LookAt = target;
            }
        }

        private void Start() =>
            _virtualCamerasID = new int[virtualCamerasObjects.Length];

        public void SwitchTo(CameraViewState viewState)
        {
            switch (viewState)
            {
                case CameraViewState.Start:
                    ActivateView(0, true);
                    break;
                case CameraViewState.Default:
                    ActivateView(1);
                    break;
                case CameraViewState.Finish:
                    ActivateView(2);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewState), viewState, null);
            }
        }

        private void ActivateView(int viewNumber, bool forceView = false)
        {
            for (int i = 0; i < _virtualCamerasID.Length; i++)
            {
                if (i == viewNumber)
                {
                    if (forceView)
                    {
                        virtualCameras[i].ForceCameraPosition(_target.position, _target.rotation);
                    }
                }

                virtualCamerasObjects[i].SetActive(i == viewNumber);
            }
        }
    }

}