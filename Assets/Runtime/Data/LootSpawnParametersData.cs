using System;
using AKhvalov.IdleFarm.Runtime.Extensions;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AKhvalov.IdleFarm.Runtime.Data
{
    [Serializable]
    public struct LootSpawnParametersData
    {
        [SerializeField] 
        private float spreading;

        [SerializeField] 
        private float animationDuration;

        [SerializeField] 
        private float jumpHeight;

        [SerializeField]
        private Ease animationEase;

        [SerializeField] 
        private Vector3 spawnRandomRotation;
        
        public float Spreading => spreading;
        public float AnimationDuration => animationDuration;
        public float JumpHeight => jumpHeight;
        public Vector3 SpawnRotation => spawnRandomRotation.RandomVector();
        public Ease AnimationEase => animationEase;
    }
}
