using System;
using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Pool
{
    public interface IPoolable
    {
        event Action<GameObject> OnObjectUsed; 
        void Activate(PoolableActivationData data);
        void Deactivate();
    }
}
