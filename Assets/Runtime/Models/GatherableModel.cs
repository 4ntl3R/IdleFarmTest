using System;

namespace Runtime.Models
{
    public class GatherableModel
    {
        public event Action<GatherableModel> OnEmpty;
        public event Action<GatherableModel> OnContaining;
        
        private readonly int _maxCapacity;
        private int _currentCapacity;

        public GatherableModel(int maxCapacity)
        {
            _maxCapacity = maxCapacity;
            _currentCapacity = _maxCapacity;
        }

        public void Use()
        {
            if (--_currentCapacity <= 0)
            {
                OnEmpty?.Invoke(this);
                return;
            }
            OnContaining?.Invoke(this);
        }

        public void Recharge()
        {
            _currentCapacity = _maxCapacity;
        }
    }
}
