using System;
using UnityEngine;

namespace Scripts.Logic.Hud
{
    public class JoystickInput : MonoBehaviour
    {
        [SerializeField] private GameObject joystickBase;
        [SerializeField] private RectTransform joystickBaseRect;
        [SerializeField] private RectTransform stickRect;
        [SerializeField] private GameStarter gameStarter;

        private Vector3 _targetPosition;
        private bool _active;

        private bool StartDrag => Input.GetMouseButtonDown(0);
        private bool EndDrag => Input.GetMouseButtonUp(0);
        
        public bool IsOnDrag => Input.GetMouseButton(0);
        
        public Vector3 NormalizedDirection { get; private set; }
        
        public event Action StartedDrag;
        public event Action EndedDrag;

        private void Start()
        {
            gameStarter.GameStarted += Activate;
        }

        private void OnDestroy()
        {
            gameStarter.GameStarted -= Activate;
        }

        private void Activate()
        {
            _active = true;
        }

        private void Update()
        {
            if(!_active)
                return;
            
            if (StartDrag)
            {
                joystickBaseRect.position = Input.mousePosition;
                joystickBase.SetActive(true);
                StartedDrag?.Invoke();
            }

            if (EndDrag)
            {
                joystickBase.SetActive(false);
                EndedDrag?.Invoke();
            }
            
            if (IsOnDrag)
            {
                RefreshTargetPosition();
                RefreshNormalizedDirection();
                
                stickRect.position = joystickBaseRect.position + _targetPosition;
            }
        }

        private void RefreshTargetPosition()
        {
            _targetPosition = Input.mousePosition - joystickBaseRect.position;
            _targetPosition = Vector3.ClampMagnitude(_targetPosition, joystickBaseRect.rect.width / 2);
        }

        private void RefreshNormalizedDirection()
        {
            NormalizedDirection = new Vector3(_targetPosition.x / (joystickBaseRect.rect.width / 2), 0, _targetPosition.y / (joystickBaseRect.rect.width / 2));
        }
    }

}