using UnityEngine;

namespace Scripts.Logic.CameraControl
{
    public class CameraTarget : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Rigidbody _thisRigidbody;
        [SerializeField] private float _rotationSpeed = 20f;
        [SerializeField] private float _xDamping;
        [SerializeField] private float _yDamping;
        [SerializeField] private float _zDamping;

        private void Start() =>
            transform.parent = null;

        private void FixedUpdate()
        {
            var targetRotation = Quaternion.RotateTowards(_thisRigidbody.rotation, _target.rotation, _rotationSpeed * Time.fixedDeltaTime);
            Vector3 rbPos = _thisRigidbody.position;
            rbPos.x = Mathf.Lerp(rbPos.x, _target.position.x, _xDamping * Time.fixedDeltaTime);
            rbPos.y = Mathf.Lerp(rbPos.y, _target.position.y, _yDamping * Time.fixedDeltaTime);
            rbPos.z = Mathf.Lerp(rbPos.z, _target.position.z, _zDamping * Time.fixedDeltaTime);
            _thisRigidbody.MoveRotation(targetRotation);
            _thisRigidbody.MovePosition(rbPos);
        }
    }
}