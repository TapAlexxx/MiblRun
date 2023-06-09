﻿using Scripts.StaticClasses;
using UnityEngine;

namespace Scripts.Logic.Unit
{

    public class UnitMovement : MonoBehaviour
    {
        [SerializeField] private float smoothness = 0.1f;
        [SerializeField] private Rigidbody unitRigidbody;
        
        private Vector3 _smoothedVelocity;
        private Vector3 _currentVelocity;
        private Vector3 _direction = Vector3.zero;
        private Vector3 _targetVelocity;
        private float _speed;
        private bool _active;

        public float Speed => _speed;
        public float NormalizedSpeed => _smoothedVelocity.magnitude / _speed;
        public Vector3 Direction => _direction;

        private void OnValidate()
        {
            if (!unitRigidbody) TryGetComponent(out unitRigidbody);
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
            _smoothedVelocity = Vector3.zero;
            _direction = Vector3.zero;
        }

        private void FixedUpdate()
        {
            if(!_active)
                return;
            
            Move();
        }

        private void Move()
        {
            _targetVelocity = Vector3.zero;

            if (_direction.sqrMagnitude > Constants.Epsilon) 
                _targetVelocity = _direction * _speed;

            _smoothedVelocity = Vector3
                .SmoothDamp(_smoothedVelocity, _targetVelocity, ref _currentVelocity, smoothness);

            var targetPosition = unitRigidbody.position + _smoothedVelocity * Time.deltaTime;
            unitRigidbody.MovePosition(targetPosition);
        }
    }

}