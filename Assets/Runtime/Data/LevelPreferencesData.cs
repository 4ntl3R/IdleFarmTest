using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Data
{
    [CreateAssetMenu(order = 1, fileName = "New LevelPreferencesData", menuName = "IdleFarm/Level Preferences")]
    public class LevelPreferencesData : ScriptableObject
    {
        [SerializeField] 
        private float playerSpeedMultiplier = 10f;

        [SerializeField] 
        private int gatherableCapacity = 1;

        [SerializeField] 
        private int lootCapacity = 40;

        [SerializeField] 
        private int lootCost = 1;

        [SerializeField] 
        private float hitBoxDuration = 0.1f;

        public float PlayerSpeedMultiplier => playerSpeedMultiplier;
        public int GatherableCapacity => gatherableCapacity;
        public int LootCapacity => lootCapacity;
        public int LootCost => lootCost;
        public float HitBoxDuration => hitBoxDuration;
    }
}
