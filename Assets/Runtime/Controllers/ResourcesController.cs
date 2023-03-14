using AKhvalov.IdleFarm.Runtime.Data;
using AKhvalov.IdleFarm.Runtime.Extensions;
using AKhvalov.IdleFarm.Runtime.Models;
using AKhvalov.IdleFarm.Runtime.Pool;
using AKhvalov.IdleFarm.Runtime.Views;
using UnityEngine;

namespace AKhvalov.IdleFarm.Runtime.Controllers
{
    public class ResourcesController
    {
        private readonly ResourcesView _view;
        private readonly ResourcesModel _model;
        private readonly GameObject _deliverySource;
        private readonly GameObject _deliveryTarget;
        private readonly GameObjectPool _deliveryPool;
        private readonly LootDeliverParametersData _data;

        private int _deliveryCounter = 0;
        private bool _isDelivering = false;
    
        public ResourcesController(ResourcesView view, ResourcesModel model, GameObjectPool deliveryLootPool, InteractionActorView player, GameObject deliveryTarget, LootDeliverParametersData deliverParametersData)
        {
            _view = view;
            _model = model;
            _deliveryPool = deliveryLootPool;
            _deliverySource = player.LootPickTarget;
            _deliveryTarget = deliveryTarget;
            _data = deliverParametersData;
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            _model.OnCoinsUpdated += _view.SetCoinValue;
            _model.OnLootUpdated += _view.SetLootValue;
            _model.OnLootCleared += DeliveryHandle;
        }

        public void UnsubscribeEvents()
        {
            _model.OnCoinsUpdated -= _view.SetCoinValue;
            _model.OnLootUpdated -= _view.SetLootValue;
            _model.OnLootCleared -= DeliveryHandle;

        }

        private void DeliveryHandle(int lootAmount)
        {
            _deliveryCounter += lootAmount;
            
            if (_isDelivering)
            {
                return;
            }

            _isDelivering = true;
            AnimateLoot();
        }

        private void AnimateLoot()
        {
            _deliveryCounter--;
            _deliveryPool.GenerateObject(CreateCurrentData());
            
            if (_deliveryCounter > 0)
            {
                DOTweenExtension.SequenceDelay(_data.TimeInterval, AnimateLoot);
                return;
            }

            _isDelivering = false;
        }

        private PoolableActivationData CreateCurrentData()
        {
            return new PoolableActivationData(_deliverySource.transform.position, _deliveryTarget.transform);
        }
    }
}
