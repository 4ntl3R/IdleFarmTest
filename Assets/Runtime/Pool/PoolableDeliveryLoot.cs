using System;
using AKhvalov.IdleFarm.Runtime.Data;
using AKhvalov.IdleFarm.Runtime.Extensions;
using DG.Tweening;
using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Pool
{
    public class PoolableDeliveryLoot : MonoBehaviour, IPoolable
    {
        public event Action<GameObject> OnObjectUsed;

        [SerializeField] 
        private AnimationData animationData;
        
        public void Activate(PoolableActivationData data)
        {
            transform.position = data.Position;
            gameObject.SetActive(true);
            
            Sequence spawnAnimation = gameObject.MoveToStaticTarget(
                data.Parent.gameObject, 
                (() => OnObjectUsed?.Invoke(gameObject)), 
                (animationData.LootDeliverParametersData));
            
            spawnAnimation.Play();
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
