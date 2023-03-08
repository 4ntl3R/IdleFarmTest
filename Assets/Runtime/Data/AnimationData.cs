using DG.Tweening;
using Runtime.Data;
using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Data
{
    [CreateAssetMenu(order = 1, fileName = "New AnimationData", menuName = "IdleFarm/AnimationData")]
    public class AnimationData : ScriptableObject
    {
        [SerializeField] 
        private GrowAnimationParametersData growParameters;

        [SerializeField] 
        private LootSpawnParametersData lootSpawnParametersData;

        [SerializeField] 
        private LootPickParametersData lootPickParametersData;

        public GrowAnimationParametersData GrowParameters => growParameters;

        public LootPickParametersData LootPickParametersData => lootPickParametersData;

        public LootSpawnParametersData LootSpawnParametersData => lootSpawnParametersData;
    }
}
