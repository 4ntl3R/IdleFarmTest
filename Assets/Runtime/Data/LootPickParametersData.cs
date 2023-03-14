using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

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
        private float jumpHeightRandomRange;

        [SerializeField][Range(0f, 1f)] 
        private float toBagScale;

        [SerializeField] 
        private Ease animationEase;
        
        [Space(10)]
        [SerializeField] 
        private float bagPunchDuration;

        [SerializeField] 
        private Vector3 bagPunchPower;

        [SerializeField] 
        private int bagPunchVibrato;

        [SerializeField] 
        private float bagPunchElastic;
        
        public float JumpHeight => jumpHeight + Random.Range(-jumpHeightRandomRange, jumpHeightRandomRange);
        public float FullAnimationDuration => fullAnimationDuration;
        public float JumpAnimationDuration => jumpAnimationDuration;
        public float MoveToTargetAnimationDuration => fullAnimationDuration - jumpAnimationDuration;
        public float ToBagScale => toBagScale;
        public Vector3 BagPunchPower => bagPunchPower;
        public float BagPunchDuration => bagPunchDuration;
        public int BagPunchVibrato => bagPunchVibrato;
        public float BagPunchElastic => bagPunchElastic;
        public Ease AnimationEase => animationEase;
    }
}