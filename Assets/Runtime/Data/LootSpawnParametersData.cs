using System;
using DG.Tweening;
using UnityEngine;

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
        
        public float Spreading => spreading;
        public float AnimationDuration => animationDuration;
        public float JumpHeight => jumpHeight;
        public Ease AnimationEase => animationEase;
    }
}
