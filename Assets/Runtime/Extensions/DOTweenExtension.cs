using System;
using AKhvalov.IdleFarm.Runtime.Data;
using DG.Tweening;
using Runtime.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AKhvalov.IdleFarm.Runtime.Extensions
{
    public static class DOTweenExtension
    {
        //todo: make structure for parameters, remove magic numbers
        public static Sequence JumpSpawn(this GameObject target, Action onCompleteCallback, 
            LootSpawnParametersData data)
        {
            var jumpDistance = (0.5f + Random.value/2) * data.Spreading;
            var jumpDestination = new Vector3(Random.value, 0, Random.value).normalized * jumpDistance;
            jumpDestination += target.transform.position;
            
            Sequence result = DOTween.Sequence();
            result
                .Append(target.transform.DOJump(jumpDestination, data.JumpHeight, 
                    1, data.AnimationDuration))
                .OnComplete(onCompleteCallback.Invoke);
            
            result.SetEase(data.AnimationEase);
            result.Pause();

            return result;
        }
        
        //todo: make structure for parameters, remove magic numbers
        public static Sequence JumpToObject(this GameObject target, GameObject destination, Action onCompleteCallback, 
            LootPickParametersData data)
        {
            Sequence result = DOTween.Sequence();
            result
                .Append(target.transform.DOJump(Vector3.up, data.JumpHeight, 1, data.JumpAnimationDuration))
                .Join(target.transform.DOMove(Vector3.up * data.JumpHeight, data.JumpAnimationDuration))
                .Append(target.transform.DOLocalJump(Vector3.zero, data.JumpHeight, 1, data.MoveToTargetAnimationDuration))
                .OnComplete(() =>
                {
                    target.transform.parent = null;
                    onCompleteCallback.Invoke();
                });

            result.SetEase(data.AnimationEase);
            result.Pause();

            return result;
        }

        public static Sequence Grow(this GameObject target, Material material, Action onCompleteCallback, 
            GrowAnimationParametersData parameters)
        {
            target.transform.localScale = parameters.StartScale;
            material.color = parameters.StartColor;
            Sequence result = DOTween.Sequence();
            result
                .Append(target.transform.DOScale(parameters.TargetScale, parameters.GrowDuration))
                .Join(material.DOColor(parameters.GrowEndColor, parameters.GrowDuration))
                .Append(target.transform.DOPunchScale(parameters.FinalPunch, parameters.FinalDuration, parameters.PunchVibrato, parameters.PunchElastic))
                .Join(material.DOColor(parameters.TargetColor, parameters.FinalDuration))
                .OnComplete(onCompleteCallback.Invoke);

            result.SetEase(parameters.Ease);
            result.Pause();

            return result;
        }

        public static void SequenceDelay(float duration, Action delayedAction)
        {
            Sequence sequence = DOTween.Sequence();
            sequence
                .AppendInterval(duration)
                .OnComplete(delayedAction.Invoke);
        }
    }
}
