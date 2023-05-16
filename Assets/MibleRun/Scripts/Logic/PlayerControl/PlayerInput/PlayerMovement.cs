using Scripts.Logic.Unit;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.PlayerInput
{

    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private UnitMovement unitMovement;
        private Vector3 _targetDirection;

        private void OnValidate()
        {
            if (!unitMovement) TryGetComponent(out unitMovement);
        }

        public void SetTargetDirection(Vector3 targetDirection)
        {
            _targetDirection = targetDirection;
        }

        private void Update()
        {
            unitMovement.SetMovementDirection(_targetDirection);
        }
    }

}