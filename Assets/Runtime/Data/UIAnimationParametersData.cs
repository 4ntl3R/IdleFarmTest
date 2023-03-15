using System;
using DG.Tweening;
using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Data
{
    [Serializable]
    public struct UIAnimationParametersData
    {
        [SerializeField] 
        private Ease textEase;

        public Ease TextEase => textEase;
    }
}
