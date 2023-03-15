using System;

namespace AKhvalov.IdleFarm.Runtime.Models
{
    public class ResourcesModel
    {
        public event Action OnLootFilled;
        public event Action<int> OnLootCleared;
        public event Action<int, int, int> OnCoinsUpdated;
        public event Action<int, int> OnLootUpdated;
        
        private readonly int _lootCost;
        private readonly int _maxLootCount;
        private int _coinsCount;
        private int _currentLootCount;
        
        public bool IsLootCapacityFilled => _currentLootCount >= _maxLootCount;

        public ResourcesModel(int lootCapacity, int lootCost)
        {
            _maxLootCount = lootCapacity;
            _currentLootCount = 0;
            _coinsCount = 0;
            _lootCost = lootCost;
        }

        public void LootIncrease()
        {
            if (++_currentLootCount >= _maxLootCount)
            {
                OnLootFilled?.Invoke();
            }
            OnLootUpdated?.Invoke(_currentLootCount, _maxLootCount);
        }

        public void ConvertLootToCoins()
        {
            if (_currentLootCount == 0)
            {
                return;
            }

            var beforeCoins = _coinsCount;
            _coinsCount += _lootCost * _currentLootCount;
            OnCoinsUpdated?.Invoke(beforeCoins, _coinsCount, _currentLootCount);

            OnLootCleared?.Invoke(_currentLootCount);
            _currentLootCount = 0;
            OnLootUpdated?.Invoke(_currentLootCount, _maxLootCount);
        }
    }
}
