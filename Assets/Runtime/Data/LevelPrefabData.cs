using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Data
{
    [CreateAssetMenu(order = 2, fileName = "New LevelPrefabData", menuName = "IdleFarm/Prefab Data")]
    public class LevelPrefabData : ScriptableObject
    {
        [SerializeField] 
        private GameObject lootPrefab;

        [SerializeField] 
        private GameObject lootDeliveryPrefab;
        
        [SerializeField] 
        private GameObject coinPrefab;

        public GameObject LootPrefab => lootPrefab;
        public GameObject LootDeliveryPrefab => lootDeliveryPrefab;
        public GameObject CoinPrefab => coinPrefab;
    }
}
