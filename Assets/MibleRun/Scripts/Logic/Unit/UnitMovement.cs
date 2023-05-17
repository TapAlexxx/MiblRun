using Scripts.StaticClasses;
using Scripts.StaticData.Level;
using UnityEngine;

namespace Scripts.Logic.Unit
{

    public class UnitMovement : MonoBehaviour
    {
        [SerializeField] private float smoothness = 0.1f;
        [SerializeField] private CharacterController characterController;

        private Vector3 _smoothedVelocity;
        private Vector3 _currentVelocity;
        private Vector3 _direction = Vector3.zero;
        private Vector3 _targetVelocity;
        private float _speed;
        private bool _active;

        public float Speed => _speed;
        public float Velocity => characterController.velocity.sqrMagnitude;
        public Vector3 Direction => _direction;

        private void OnValidate()
        {
            if (!characterController) TryGetComponent(out characterController);
        }

        public void Initialize(float speed) => 
            _speed = speed;

        public void SetMovementDirection(Vector3 direction)
        {
            if(!_active) 
                return;
            _direction = direction;
        }

        public void Activate() => 
            _active = true;

        public void Disable()
        {
            _active = false;
            _direction = Vector3.zero;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            _targetVelocity = Vector3.zero;

            if (_direction.sqrMagnitude > Constants.Epsilon) 
                _targetVelocity = _direction * _speed;

            _smoothedVelocity = Vector3
                .SmoothDamp(_smoothedVelocity, _targetVelocity + Physics.gravity, ref _currentVelocity, smoothness);
            
            characterController
                .Move(_smoothedVelocity * Time.deltaTime);
        }
    }

}