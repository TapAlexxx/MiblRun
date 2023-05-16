using UnityEngine;

namespace Scripts.Logic.Unit
{

    public class UnitMovementAnimation : MonoBehaviour
    {
        [SerializeField] private UnitMovement unitMovement;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Animator animator;
        
        private void OnValidate()
        {
            if (!unitMovement) TryGetComponent(out unitMovement);
            if (!characterController) TryGetComponent(out characterController);
            if (!animator) TryGetComponent(out animator);
        }

        private void Update()
        {
            animator.SetFloat("Movement", characterController.velocity.magnitude / unitMovement.Speed);
        }
    }

}