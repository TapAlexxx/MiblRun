using System;
using Scripts.Extensions;
using Scripts.StaticClasses;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
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
        private JobHandle _directionJobHandle;
        private NativeArray<Vector3> _directionResult;

        public float Speed => _speed;
        public Vector3 Direction => _direction;
        public float NormalizedSpeed => _smoothedVelocity.magnitude / _speed;

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
            _direction = Vector3.zero;
        }

        private void FixedUpdate()
        {
            if(!_active)
                return;
            
            _targetVelocity = Vector3.zero;
            if (_direction.sqrMagnitude < Constants.Epsilon) 
                return;
            
            _directionResult = new NativeArray<Vector3>(2, Allocator.TempJob);
            DirectionJob job = new DirectionJob
            {
                Direction = _direction,
                Speed = _speed,
                SmoothedVelocity = _smoothedVelocity,
                TargetVelocity = _targetVelocity, 
                CurrentVelocity = _currentVelocity,
                DeltaTime = Time.deltaTime,
                RigidBodyPos = unitRigidbody.position,
                CurrentPosition = transform.position,
                Result = _directionResult
            };
            _directionJobHandle = job.Schedule();
            _directionJobHandle.Complete();

            _smoothedVelocity = _directionResult[1];
            unitRigidbody.MovePosition(_directionResult[0]);

            _directionResult.Dispose();
        }

        /*
        private void FixedUpdate()
        {
            if(!_active)
                return;
            //Move();

            _targetVelocity = Vector3.zero;
            if (_direction.sqrMagnitude < Constants.Epsilon) 
                return;
            
            _directionResult = new NativeArray<Vector3>(3, Allocator.TempJob);
            DirectionJob job = new DirectionJob
            {
                Direction = _direction,
                Speed = _speed,
                SmoothedVelocity = _smoothedVelocity,
                TargetVelocity = _targetVelocity, 
                CurrentVelocity = _currentVelocity,
                DeltaTime = Time.deltaTime,
                RigidBodyPos = unitRigidbody.position,
                CurrentPosition = transform.position,
                Result = _directionResult
            };
            _directionJobHandle = job.Schedule();
            _directionJobHandle.Complete();

            Debug.Log($"{unitRigidbody.position} -> {_directionResult[0]}");
            unitRigidbody.MovePosition(_directionResult[0]);
            _smoothedVelocity = _directionResult[1];
            _targetVelocity = _directionResult[2];
            
            _directionResult.Dispose();
        }
        */

        [BurstCompile]
        private struct DirectionJob : IJob
        {
            public Vector3 Direction;
            public float   Speed;
            public Vector3 SmoothedVelocity;
            public Vector3 TargetVelocity;
            public Vector3 CurrentVelocity;
            public float   DeltaTime;
            public Vector3 RigidBodyPos;
            public Vector3 CurrentPosition;

            public NativeArray<Vector3> Result;

            public void Execute()
            {
                TargetVelocity = Direction * Speed;
                
                SmoothedVelocity = Vector3
                    .SmoothDamp(SmoothedVelocity, TargetVelocity, ref CurrentVelocity, 0.1f,
                        float.PositiveInfinity, DeltaTime);
                Vector3 targetPosition = RigidBodyPos + SmoothedVelocity * DeltaTime;
                targetPosition.SetY(CurrentPosition.y);
                
                Result[0] = targetPosition;
                Result[1] = SmoothedVelocity;
            }
        }

        private void Move()
        {
            _targetVelocity = Vector3.zero;

            if (_direction.sqrMagnitude > Constants.Epsilon) 
                _targetVelocity = _direction * _speed;

            _smoothedVelocity = Vector3
                .SmoothDamp(_smoothedVelocity, _targetVelocity, ref _currentVelocity, smoothness);

            Vector3 targetPosition = unitRigidbody.position + _smoothedVelocity * Time.deltaTime;
            targetPosition.SetY(transform.position.y);
            unitRigidbody.MovePosition(targetPosition);
        }
    }

}