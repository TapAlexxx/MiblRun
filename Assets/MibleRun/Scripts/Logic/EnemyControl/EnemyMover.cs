using System;
using Scripts.Logic.PlayerControl;
using Scripts.Logic.Unit;
using Unity.Burst;
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

        private void Update()
        {
            if (_player == null)
                return;

            if (!_playerHealth.IsAlive)
            {
                StopMove();
                return;
            }

            Vector3 target = CalculateTargetDirection();
            unitMovement.SetMovementDirection(target);
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