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
            
            Sequence spawnAnimation = gameObject.JumpSpawn(_reactorView.Activate, animationData.LootSpawnParametersData);
            
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
                actor.LootPickTarget, 
                () => OnObjectUsed?.Invoke(gameObject),
                animationData.LootPickParametersData);
            jumpAnimation.Play();
        }

        private void OnDestroy()
        {
            _reactorView.OnInteraction -= StartUsing;
        }
    }
}
