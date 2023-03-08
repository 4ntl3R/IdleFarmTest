using System;
using AKhvalov.IdleFarm.Runtime.Data;
using AKhvalov.IdleFarm.Runtime.Extensions;
using AKhvalov.IdleFarm.Runtime.Views;
using DG.Tweening;
using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Pool
{
    [RequireComponent(typeof(InteractionReactorView))]
    public class PoolableLoot : InitiatableMonoBehaviour, IPoolable
    {
        public event Action<GameObject> OnObjectUsed;

        [SerializeField] 
        private AnimationData animationData;

        private InteractionReactorView _reactorView;
        
        public void Activate(Vector3 position)
        {
            Initiate();
            transform.position = position;
            gameObject.SetActive(true);
            
            Sequence spawnAnimation = gameObject.JumpSpawn(
                onCompleteCallback: _reactorView.Activate, 
                jumpPower: animationData.LootSpawnJumpHeight,
                maxDistance: animationData.LootSpawnSpreading,
                duration: animationData.LootSpawnAnimationDuration,
                ease: animationData.LootSpawnAnimationEase);
            
            spawnAnimation.Play();
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
        
        protected override void Initiate()
        {
            base.Initiate();
            _reactorView = GetComponent<InteractionReactorView>();
            _reactorView.OnInteraction += StartUsing;
        }

        private void StartUsing(InteractionActorView actor, InteractionReactorView reactorView)
        {
            Sequence jumpAnimation = reactorView.gameObject.JumpToObject(
                destination: actor.LootPickTarget, 
                onCompleteCallback: () => OnObjectUsed?.Invoke(gameObject),
                jumpPower: animationData.LootPickJumpHeight,
                duration: animationData.LootPickAnimationDuration,
                ease: animationData.LootPickAnimationEase);
            jumpAnimation.Play();
        }

        private void OnDestroy()
        {
            _reactorView.OnInteraction -= StartUsing;
        }
    }
}
