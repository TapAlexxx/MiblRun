using System;
using Scripts.Logic.PlayerControl;
using Scripts.Logic.Unit;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace Scripts.Logic.EnemyControl
{

    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private UnitMovement unitMovement;
        
        private Transform _player;
        private PlayerHealth _playerHealth;
        
        private void OnValidate()
        {
            if (!unitMovement) TryGetComponent(out unitMovement);
        }

        public void Initialize(Transform player)
        {
            _player = player;
            _playerHealth = _player.GetComponent<PlayerHealth>();
        }

        private struct DirectionJob : IJob
        {
            public Vector3 PlayerPosition;
            public Vector3 EnemyPosition;
            public NativeArray<Vector3> Result;
            
            public void Execute()
            {
                Vector3 targetDirection = PlayerPosition - EnemyPosition;
                Result[0] = targetDirection.normalized;
            }
        }

        private void Update()
        {
            if(_player == null)
                return;

            if (!_playerHealth.IsAlive)
            {
                StopMove();
                return;
            }

            NativeArray<Vector3> directionResult = new NativeArray<Vector3>(1, Allocator.TempJob);
            DirectionJob job = new DirectionJob
            {
                PlayerPosition = _player.position,
                EnemyPosition =  transform.position,
                Result = directionResult
            };
            JobHandle directionJobHandle = job.Schedule();
            
            directionJobHandle.Complete();
            unitMovement.SetMovementDirection(directionResult[0]);
            directionResult.Dispose();
        }

        private Vector3 CalculateTargetDirection()
        {
            Vector3 targetDirection = _player.position - transform.position;
            targetDirection = targetDirection.normalized;
            return targetDirection;
        }

        private void StopMove()
        {
            unitMovement.SetMovementDirection(Vector3.zero);
            _player = null;
        }

        public void StartMove()
        {
            unitMovement.Activate();
        }
    }

}