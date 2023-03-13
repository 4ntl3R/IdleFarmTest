using System;
using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Data
{
    [Serializable]
    public struct PlayerAnimationParametersData
    {
        [SerializeField] 
        [Range(0, 1f)]
        private float velocityMovingThreshold;

        [SerializeField] 
        [Range(0, 1f)]
        private float velocityRunningThreshold;

        [SerializeField] 
        private float walkingMinSpeed;

        [SerializeField] 
        private float walkingMaxSpeed;

        [SerializeField] 
        private float runningMinSpeed;

        [SerializeField] 
        private float runningMaxSpeed;

        [SerializeField] 
        private float gatheringDuration;
        

        public float VelocityMovingThreshold => velocityMovingThreshold;
        public float VelocityRunningThreshold => velocityRunningThreshold;
        public float WalkingMinSpeed => walkingMinSpeed;
        public float WalkingMaxSpeed => walkingMaxSpeed;
        public float RunningMinSpeed => runningMinSpeed;
        public float RunningMaxSpeed => runningMaxSpeed;
        public float GatheringDuration => gatheringDuration;
    }
}
