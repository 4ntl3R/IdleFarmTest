using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Pool
{
    public struct PoolableActivationData
    {
        public PoolableActivationData(Vector3 position, Transform parent = null)
        {
            Position = position;
            Parent = parent;
        }

        public Transform Parent { get; }
        public Vector3 Position { get; }
    }
}
