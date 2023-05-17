using System;
using Scripts.Logic.Unit;
using UnityEngine;

namespace Scripts.Logic.EnemyControl
{

    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private UnitMovement unitMovement;
        
        private Transform _player;

        private void OnValidate()
        {
            if (!unitMovement) TryGetComponent(out unitMovement);
        }

        public void Initialize(Transform player)
        {
            _player = player;
        }

        private void Update()
        {
            if(_player == null)
                return;
            
            Vector3 targetDirection = _player.position - transform.position;
            targetDirection = targetDirection.normalized;
            unitMovement.SetMovementDirection(targetDirection);
        }
    }

}