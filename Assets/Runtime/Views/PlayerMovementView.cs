using System;
using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Controllers
{
    public class PlayerMovementView : MonoBehaviour
    {
        private static float magnitudeDeadZone = 0.01f;
        
        private float _speedMultiplier;
        
        private Rigidbody _rigidbody;

        public void Inject(float speedMultiplier)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _speedMultiplier = speedMultiplier;
        }

        public void ChangeVelocity(Vector2 value)
        {
            if (Math.Abs(value.magnitude) < magnitudeDeadZone)
            {
                _rigidbody.velocity = Vector3.zero;
                return;
            }
            
            var eulerAngle = Vector2.SignedAngle(Vector2.up, new Vector2(-value.x, value.y));
            _rigidbody.MoveRotation(Quaternion.Euler(0, eulerAngle, 0));
            _rigidbody.velocity = _speedMultiplier * value.magnitude * new Vector3(value.x, 0, value.y);
        }
    }
}
