using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime
{
    public abstract class InitiatableMonoBehaviour : MonoBehaviour
    {
        private bool _isInitiated = false;

        protected virtual void Initiate()
        {
            if (_isInitiated)
            {
                return;
            }
            _isInitiated = true;
        }
    }
}
