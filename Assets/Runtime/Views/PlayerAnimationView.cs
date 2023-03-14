using System;
using AKhvalov.IdleFarm.Runtime.Controllers;
using AKhvalov.IdleFarm.Runtime.Data;
using AKhvalov.IdleFarm.Runtime.Data.Enums;
using JetBrains.Annotations;
using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Views
{
    public class PlayerAnimationView : MonoBehaviour
    {
        private const float VelocityMax = 1f;
        
        private static readonly int MoveAnimationSpeedID = Animator.StringToHash("MoveAnimationSpeed");
        private static readonly int MovingTypeID = Animator.StringToHash("MovingType");
        private static readonly int InteractID = Animator.StringToHash("Interact");

        public event Action OnGather;
        
        [SerializeField]
        private Animator animator;

        private InputJoystickController _joystick;
        private PlayerAnimationParametersData _parameters;

        [UsedImplicitly]
        public void GatheringAnimationEventResolve()
        {
            OnGather?.Invoke();
        }

        public void Inject(InputJoystickController joystickController, PlayerAnimationParametersData parametersData)
        {
            _joystick = joystickController;
            _parameters = parametersData;
            SubscribeEvents();
        }

        public void StartGatherAnimation()
        {
            animator.SetTrigger(InteractID);
        }

        private void OnDestroy()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _joystick.OnJoystickDrag += HandleMoving;
        }

        private void UnsubscribeEvents()
        {
            _joystick.OnJoystickDrag -= HandleMoving;
        }

        private void HandleMoving(Vector2 moveVector)
        {
            var velocity = moveVector.magnitude;
            
            if (velocity > _parameters.VelocityRunningThreshold)
            {
                SetMoveAnimationSpeed(
                    velocity: velocity, 
                    minThreshold:_parameters.VelocityRunningThreshold, 
                    maxThreshold: VelocityMax,
                    minSpeed: _parameters.RunningMinSpeed,
                    maxSpeed: _parameters.RunningMaxSpeed);
                    
                SetMoveType(MovingType.Run);
                return;
            }
            
            if (velocity > _parameters.VelocityMovingThreshold)
            {
                SetMoveAnimationSpeed(
                    velocity: velocity, 
                    minThreshold:_parameters.VelocityMovingThreshold, 
                    maxThreshold: _parameters.VelocityRunningThreshold,
                    minSpeed: _parameters.WalkingMinSpeed,
                    maxSpeed: _parameters.WalkingMaxSpeed);
                    
                SetMoveType(MovingType.Walk);
                return;
            }
            SetMoveType(MovingType.Stand);
        }

        private void SetMoveAnimationSpeed(float velocity, float minThreshold, float maxThreshold, float minSpeed, float maxSpeed)
        {
            var interpolationValue = (velocity - minThreshold) / (maxThreshold - minThreshold);
            var animationSpeed = Mathf.Lerp(minSpeed, maxSpeed, interpolationValue);

            animator.SetFloat(MoveAnimationSpeedID, animationSpeed);
        }

        private void SetMoveType(MovingType movingType)
        {
            animator.SetInteger(MovingTypeID, (int)movingType);
        }
    }
}
