using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AKhvalov.IdleFarm.Runtime.Data
{
    [Serializable]
    public struct LootDeliverParametersData
    {
        [SerializeField] 
        private float duration;

        [SerializeField] 
        private Ease ease;

        [SerializeField] 
        private float basicTimeInterval;

        [SerializeField]
        private float intervalRandomRange;

        public float Duration => duration;
        public Ease Ease => ease;

        public float TimeInterval =>
            basicTimeInterval + Random.Range(-intervalRandomRange, intervalRandomRange);
    }
}
