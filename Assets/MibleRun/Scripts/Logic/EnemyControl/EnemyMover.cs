using System;
using Scripts.Logic.PlayerControl;
using Scripts.Logic.Unit;
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
            if(_player == null)
                return;

            if (!_playerHealth.IsAlive)
            {
                unitMovement.SetMovementDirection(Vector3.zero);
                _player = null;
                return;
            }
            
            Vector3 targetDirection = _player.position - transform.position;
            targetDirection = targetDirection.normalized;
            unitMovement.SetMovementDirection(targetDirection);
        }

        public void StartMove()
        {
            unitMovement.Activate();
        }
    }

}