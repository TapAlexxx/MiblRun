using UnityEngine;

namespace Scripts.Logic.Unit
{

    public class UnitMovementAnimation : MonoBehaviour
    {
        [SerializeField] private UnitMovement unitMovement;
        [SerializeField] private Animator animator;
        
        private void OnValidate()
        {
            if (!unitMovement) TryGetComponent(out unitMovement);
            if (!animator) TryGetComponent(out animator);
        }

        private void Update()
        {
            animator.SetFloat("Movement", unitMovement.NormalizedSpeed);
        }
    }

}