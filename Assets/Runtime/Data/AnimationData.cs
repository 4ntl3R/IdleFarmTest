using DG.Tweening;
using Runtime.Data;
using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Data
{
    [CreateAssetMenu(order = 1, fileName = "New AnimationData", menuName = "IdleFarm/AnimationData")]
    //todo: make structure for parameters
    public class AnimationData : ScriptableObject
    {
        [Header("Loot spawning animation parameters")]
        [SerializeField] 
        private float lootSpawnSpreading = 1f;

        [SerializeField] 
        private float lootSpawnAnimationDuration = 0.5f;

        [SerializeField] 
        private float lootSpawnJumpHeight = 1f;

        [SerializeField] 
        private Ease lootSpawnAnimationEase = Ease.InSine;

        [Space(10)] 
        [Header("Loot picking animation parameters")] 
        [SerializeField]
        private float lootPickAnimationDuration = 1f;
        
        [SerializeField] 
        private float lootPickJumpHeight = 1f;
        
        [SerializeField] 
        private Ease lootPickAnimationEase = Ease.InSine;

        [SerializeField] 
        private GrowAnimationParametersData growParameters;

        public float LootSpawnSpreading => lootSpawnSpreading;
        public float LootSpawnAnimationDuration => lootSpawnAnimationDuration;
        public float LootSpawnJumpHeight => lootSpawnJumpHeight;
        public Ease LootSpawnAnimationEase => lootSpawnAnimationEase;

        public float LootPickJumpHeight => lootPickJumpHeight;
        public float LootPickAnimationDuration => lootPickAnimationDuration;
        public Ease LootPickAnimationEase => lootPickAnimationEase;

        public GrowAnimationParametersData GrowParameters => growParameters;
    }
}
