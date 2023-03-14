using System;
using AKhvalov.IdleFarm.Runtime.Data;
using AKhvalov.IdleFarm.Runtime.Extensions;
using AKhvalov.IdleFarm.Runtime.Views;
using DG.Tweening;
using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Pool
{
    [RequireComponent(typeof(InteractionReactorView))]
    public class PoolableLoot : MonoBehaviour, IPoolable
    {
        public event Action<GameObject> OnObjectUsed;

        [SerializeField] 
        private AnimationData animationData;

        private InteractionReactorView _reactorView;
        
        private void Awake()
        {
            _reactorView = GetComponent<InteractionReactorView>();
            _reactorView.OnInteractionEnd += StartUsing;
        }
        
        public void Activate(Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
            
            Sequence spawnAnimation = gameObject.JumpSpawn(_reactorView.Activate, animationData.LootSpawnParametersData);
            
            spawnAnimation.Play();
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        private void StartUsing(InteractionActorView actor, InteractionReactorView reactorView)
        {
            Sequence jumpAnimation = reactorView.gameObject.JumpToObject(
                actor.LootPickTarget, 
                () => OnObjectUsed?.Invoke(gameObject),
                animationData.LootPickParametersData,
                actor.LootPickTargetScale);
            jumpAnimation.Play();
        }

        private void OnDestroy()
        {
            _reactorView.OnInteractionEnd -= StartUsing;
        }
    }
}
