using System;
using Cinemachine;
using UnityEngine;

namespace Scripts.Logic.CameraControl
{
    public class CameraStateChanger : MonoBehaviour
    {
        [SerializeField] private GameObject[] _virtualCamerasObjects;
        [SerializeField] private CinemachineVirtualCamera[] _virtualCameras;

        private int[] _virtualCamerasID;
        
        private Transform _target;

        public void Initialize(Transform target) => 
            _target = target;

        private void Start() => 
            _virtualCamerasID = new int[_virtualCamerasObjects.Length];

        public void SwitchTo(CameraViewState viewState, Transform target)
        {
            switch (viewState)
            {
                case CameraViewState.Start:
                    ActivateView(target, 0, true);
                    break;
                case CameraViewState.Default:
                    ActivateView(target, 1);
                    break;
                case CameraViewState.Finish:
                    ActivateView(target, 2);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewState), viewState, null);
            }
        }

        private void ActivateView(Transform target, int viewNumber, bool forceView = false)
        {
            for (int i = 0; i < _virtualCamerasID.Length; i++)
            {
                if (i == viewNumber)
                {
                    _virtualCameras[i].Follow = target;
                    _virtualCameras[i].LookAt = target;
                    _virtualCamerasObjects[i].SetActive(true);
                    continue;
                }
                _virtualCamerasObjects[i].SetActive(false);
                if(forceView)
                    _virtualCameras[i].ForceCameraPosition(target.position, target.rotation);
            }
        }
    }
}