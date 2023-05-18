using Scripts.StaticClasses;
using UnityEngine;

namespace Scripts.Logic.Unit
{

    public class MovementControlRotation : MonoBehaviour
    {
        [SerializeField] private UnitRotation unitRotation;
        [SerializeField] private UnitMovement unitMovement;
        
        private void OnValidate()
        {
            if (!unitMovement) TryGetComponent(out unitMovement);
            if (!unitRotation) TryGetComponent(out unitRotation);
        }

        private void Update()
        {
            if (unitMovement.NormalizedSpeed > Constants.Epsilon) 
                unitRotation.SetRotationDirection(unitMovement.Direction);
        }
    }

}