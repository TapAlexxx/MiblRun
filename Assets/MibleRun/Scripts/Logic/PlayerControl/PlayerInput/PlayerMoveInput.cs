using Scripts.Logic.Unit;
using UnityEngine;

namespace Scripts.Logic.PlayerControl.PlayerInput
{

    public class PlayerMoveInput : MonoBehaviour
    {
        [SerializeField] private UnitMovement unitMovement;

        private void OnValidate()
        {
            if (!unitMovement) TryGetComponent(out unitMovement);
        }

        private void Update()
        {
            Vector3 targetDirection;
            var axisX = Input.GetAxis("Horizontal");
            var axisY = Input.GetAxis("Vertical");
            
            targetDirection = Vector3.forward * axisY + Vector3.right * axisX;

            unitMovement.SetMovementDirection(targetDirection);
        }
    }

}