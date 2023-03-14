using System;
using AKhvalov.IdleFarm.Runtime.Data;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Runtime.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AKhvalov.IdleFarm.Runtime.Extensions
{
    public static class DOTweenExtension
    {
        private const int DefaultJumpsAmount = 1;
        
        public static Sequence JumpSpawn(this GameObject target, Action onCompleteCallback, 
            LootSpawnParametersData data)
        {
            var jumpDistance = Random.value * data.Spreading;
            var jumpDestination = new Vector3(Random.value, 0, Random.value).normalized * jumpDistance;
            jumpDestination += target.transform.position;
            
            Sequence result = DOTween.Sequence();
            result
                .Append(target.transform.DOJump(jumpDestination, data.JumpHeight, 
                    DefaultJumpsAmount, data.AnimationDuration))
                .OnComplete(onCompleteCallback.Invoke);
            
            result.SetEase(data.AnimationEase);
            result.Pause();

            return result;
        }
        
        public static Sequence JumpToObject(this GameObject target, GameObject destination, Action onCompleteCallback, 
            LootPickParametersData data, Vector3 bagScale)
        {
            Vector3 startScale = target.transform.localScale;
            Vector3 toBagScaleMaxValue = startScale.TermDivision(bagScale);

            Sequence result = DOTween.Sequence();
            result
                .Append(target.transform.DOMoveToTarget(destination.transform, data.MoveToTargetAnimationDuration,
                    data.AnimationEase))
                .AppendCallback(() => target.transform.SetParent(destination.transform))
                .Append(target.transform.DOLocalJump(Vector3.zero, data.JumpHeight, DefaultJumpsAmount,
                    data.JumpAnimationDuration))
                .Join(target.transform.DOScale(Vector3.Lerp(Vector3.zero, toBagScaleMaxValue, data.ToBagScale),
                    data.JumpAnimationDuration))
                .AppendCallback(() =>
                {
                    onCompleteCallback.Invoke();
                    target.transform.SetParent(null);
                    target.transform.localScale = startScale;
                })
                .Append(destination.transform.DOPunchScale(data.BagPunchPower, data.BagPunchDuration,
                    data.BagPunchVibrato, data.BagPunchElastic))
                .OnComplete((() => destination.transform.localScale = bagScale));

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
        
        public static TweenerCore<float, float, FloatOptions> DOMoveToTarget(this Transform target, Transform destination, float duration, Ease ease)
        {
            var startPosition = target.transform.position;
            var startMagnitude = (destination.position - startPosition).magnitude;
            var toTween = DOTween
                .To(
                () => ((destination.position - target.position).magnitude / startMagnitude), 
                x => target.position = Vector3.Lerp(startPosition, destination.transform.position, (1 - x)),
            0, duration)
                .SetEase(ease);
            return toTween;
        }
    }
}
