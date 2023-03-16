using System;
using DG.Tweening;
using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Data
{
    [Serializable]
    public struct UIAnimationParametersData
    {
        [SerializeField] 
        private float counterJumpPower;

        [SerializeField] 
        private Ease receiverEase;

        [SerializeField] 
        private Ease textEase;

        public Ease TextEase => textEase;
        public float CounterJumpPower => counterJumpPower;
        public Ease ReceiverEase => receiverEase;
    }
}
