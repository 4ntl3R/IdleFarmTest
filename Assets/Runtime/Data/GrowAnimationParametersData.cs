using System;
using DG.Tweening;
using UnityEngine;

namespace Runtime.Data
{
    [Serializable]
    public struct GrowAnimationParametersData
    {
        [SerializeField]
        private Vector3 targetScale;
        [SerializeField]
        private Color targetColor;
        [SerializeField]
        private Color startColor;
        [SerializeField]
        private float fullDuration;
        [SerializeField]
        private float finalDuration;
        [SerializeField]
        private float growMaxColorLerp;
        [SerializeField]
        private Ease ease;
        [SerializeField]
        private Vector3 finalPunch;

        public Vector3 TargetScale => targetScale;
        public Vector3 StartScale => new Vector3(targetScale.x, 0, targetScale.z);
        public Color TargetColor => targetColor;
        public Color GrowEndColor => Color.Lerp(startColor, targetColor, growMaxColorLerp);
        public Color StartColor => startColor;
        public float FinalDuration => finalDuration;
        public float GrowDuration => fullDuration - finalDuration;
        public Ease Ease => ease;
        public Vector3 FinalPunch => finalPunch;
        
        public int PunchVibrato => 1;
        
        public float PunchElastic => 0.5f;

    }
}
