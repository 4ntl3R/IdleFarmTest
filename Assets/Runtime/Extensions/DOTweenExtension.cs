using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AKhvalov.IdleFarm.Runtime.Extensions
{
    public static class DOTweenExtension
    {
        public static Sequence JumpSpawn(this GameObject target, Action onCompleteCallback, 
            float maxDistance, 
            float jumpPower, 
            float duration, 
            Ease ease)
        {
            var jumpDistance = (0.5f + Random.value/2) * maxDistance;
            var jumpDestination = new Vector3(Random.value, 0, Random.value).normalized * jumpDistance;
            
            Sequence result = DOTween.Sequence();
            result
                .Append(target.transform.DOJump(jumpDestination, jumpPower, 1, duration))
                .OnComplete(onCompleteCallback.Invoke);
            
            result.SetEase(ease);
            result.Pause();

            return result;
        }
        
        public static Sequence JumpToObject(this GameObject target, GameObject destination, Action onCompleteCallback, 
            float jumpPower, 
            float duration, 
            Ease ease)
        {
            Sequence result = DOTween.Sequence();
            result
                .Append(target.JumpSpawn(
                    onCompleteCallback:() => target.transform.SetParent(destination.transform),
                    jumpPower: jumpPower,
                    maxDistance: 1f,
                    duration: duration / 3, 
                    ease: ease))
                
                .Join(target.transform.DOMove(Vector3.up * jumpPower, duration / 3))
                .Append(target.transform.DOLocalJump(Vector3.zero, jumpPower, 1, duration * 2 / 3))
                .OnComplete(() =>
                {
                    target.transform.parent = null;
                    onCompleteCallback.Invoke();
                });

            result.SetEase(ease);
            result.Pause();

            return result;
        }
    }
}
