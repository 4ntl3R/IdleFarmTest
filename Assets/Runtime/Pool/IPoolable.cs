using System;
using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Pool
{
    public interface IPoolable
    {
        event Action<GameObject> OnObjectUsed; 
        void Activate(Vector3 position);
        void Deactivate();
    }
}
