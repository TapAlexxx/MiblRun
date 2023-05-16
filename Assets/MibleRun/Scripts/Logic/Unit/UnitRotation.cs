using Scripts.StaticClasses;
using Scripts.StaticData.Level;
using UnityEngine;

namespace Scripts.Logic.Unit
{

    public class UnitRotation : MonoBehaviour
    {
        private Vector3 _direction = Vector3.zero;
        private float _rotationSpeed;

        public void Initialize(PlayerStaticData playerStaticData) => 
            _rotationSpeed = playerStaticData.RotationSpeed;

        public void SetRotationDirection(Vector3 direction) => 
            _direction = direction;

        private void Update() => 
            Rotate();

        private void Rotate()
        {
            if (_direction.sqrMagnitude > Constants.Epsilon)
            {
                Quaternion targetRotation = Quaternion
                    .LookRotation(_direction);

                transform.rotation = Quaternion
                    .Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
            }
        }
    }

}