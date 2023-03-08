using System;
using DG.Tweening;
using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Data
{
    [Serializable]
    public struct LootPickParametersData
    {
        [SerializeField]
        private float fullAnimationDuration;

        [SerializeField] 
        private float jumpAnimationDuration;
        
        [SerializeField] 
        private float jumpHeight;
        
        [SerializeField] 
        private Ease animationEase;
        
        public float JumpHeight => jumpHeight;
        public float FullAnimationDuration => fullAnimationDuration;
        public float JumpAnimationDuration => jumpAnimationDuration;
        public float MoveToTargetAnimationDuration => fullAnimationDuration - jumpAnimationDuration;
        public Ease AnimationEase => animationEase;
    }
}