using System;
using Cinemachine;
using UnityEngine;

namespace Scripts.Logic.CameraControl
{

    public class CameraStateChanger : MonoBehaviour
    {
        [SerializeField] private GameObject[] virtualCamerasObjects;
        [SerializeField] private CinemachineVirtualCamera[] virtualCameras;

        private int[] _virtualCamerasID;

        private Transform _target;

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