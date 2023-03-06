using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        private float _speedMultiplier;
        
        private Rigidbody _rigidbody;

        public void Inject(float speedMultiplier)
        {
            _rigidbody = GetComponent<Rigidbody>();
            _speedMultiplier = speedMultiplier;
        }

        public void ChangeVelocity(Vector2 value)
        {
            var velocityVector = new Vector3(value.x * _speedMultiplier, 0, value.y * _speedMultiplier);
            _rigidbody.velocity = velocityVector;
        }
    }
}
